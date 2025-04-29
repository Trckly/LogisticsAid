import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { Trip } from '../../../../shared/models/trip.model';
import { TripService } from '../../../../shared/services/trip.service';
import { ErrorPopupService } from '../../../../shared/services/error-popup.service';
import {
  merge,
  startWith,
  switchMap,
  catchError,
  map,
  of as observableOf,
  Subject,
} from 'rxjs';
import { PaginatedResponse } from '../../../../shared/models/paginated-response.model';
import { RoutePoint } from '../../../../shared/models/route-point.model';
import { ContactInfo } from '../../../../shared/models/contact-info.model';
import { Transport } from '../../../../shared/models/transport.model';
import { SelectionModel } from '@angular/cdk/collections';
import { SuccessPopupService } from '../../../../shared/services/success-popup.service';
import { SeparatedNumberPipe } from '../../../../shared/pipes/separated-number.pipe';
import { RoutePointTrip } from '../../../../shared/models/route-point-trip';
import { routes } from '../../../../app.routes';

class TableData {
  trip: Trip = new Trip();
  routePoints: RoutePoint[] = [];
}

@Component({
  selector: 'app-trips-page',
  imports: [
    CommonModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatCheckboxModule,
    SeparatedNumberPipe,
  ],
  templateUrl: './trips-page.component.html',
  styleUrl: './trips-page.component.scss',
  providers: [SeparatedNumberPipe],
})
export class TripsPageComponent implements OnInit, AfterViewInit {
  paginatedTrips: PaginatedResponse<Trip> = new PaginatedResponse<Trip>();
  routePoints: RoutePoint[] = [];

  tableData: TableData[] = [];

  selectedFilter: string = '';

  isEditMode: boolean = false;
  selection = new SelectionModel<TableData>(true, []);

  displayedColumns: string[] = [
    'select',
    'ID',
    'loadingDate',
    'unloadingDate',
    'customerPrice',
    'carrierPrice',
    'profit',
    'loadingAddress',
    'unloadingAddress',
    'driverName',
    'licensePlate',
  ];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  pageSize: number = 20;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  private reloadSubject = new Subject<void>();

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private tripService: TripService,
    private errorPopupService: ErrorPopupService,
    private successPopupService: SuccessPopupService
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page, this.reloadSubject)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.tripService
            .getTrips(this.paginator.pageIndex, this.paginator.pageSize)
            .pipe(catchError(() => observableOf(null)));
        }),
        switchMap((tripData: PaginatedResponse<Trip> | null) => {
          if (tripData === null) {
            return observableOf({ trips: null, routePoints: null });
          }

          // Second query: Get route points using trip IDs
          const tripIds: string[] = tripData.items.map((trip) => trip.id);
          return this.tripService.getRoutePointsById(tripIds).pipe(
            map((routePointsData) => {
              return {
                trips: tripData,
                routePoints: routePointsData,
              };
            }),
            catchError(() =>
              observableOf({
                trips: tripData,
                routePoints: [],
              })
            )
          );
        }),
        switchMap((tripsAndRoutePoints) => {
          if (tripsAndRoutePoints.routePoints === null) {
            return observableOf({
              trips: null,
              routePoints: [],
              routePointsTrips: [],
            });
          }

          const tripIds: string[] = tripsAndRoutePoints.trips.items.map(
            (trip) => trip.id
          );
          return this.tripService.getRoutePointTrips(tripIds).pipe(
            map((routePointsTrips) => {
              return {
                trips: tripsAndRoutePoints.trips,
                routePoints: tripsAndRoutePoints.routePoints,
                routePointsTrips: routePointsTrips,
              };
            }),
            catchError(() =>
              observableOf({
                trips: tripsAndRoutePoints.trips,
                routePoints: tripsAndRoutePoints.routePoints,
                routePointsTrips: [],
              })
            )
          );
        }),
        map((result) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;

          const { trips, routePoints, routePointsTrips } = result;
          this.isRateLimitReached = trips === null;

          if (trips === null) {
            return { tripItems: [], routePointItems: [], routePointsTrips: [] };
          }

          // Update results length for pagination
          this.resultsLength = trips.totalCount;

          return {
            tripItems: trips.items,
            routePointItems: routePoints,
            routePointsTrips: routePointsTrips,
          };
        })
      )
      .subscribe((data) => {
        this.paginatedTrips.items = data.tripItems;
        this.routePoints = data.routePointItems;
        const routePointsTrips: RoutePointTrip[] =
          data.routePointsTrips as RoutePointTrip[];

        this.tableData = [];

        this.paginatedTrips.items.forEach((trip) => {
          trip.loadingDate = new Date(trip.loadingDate);
          trip.unloadingDate = new Date(trip.unloadingDate);
        });

        this.paginatedTrips.items.forEach((item) => {
          // 1. Find all routePointId values associated with this trip
          const relatedRoutePointIds = routePointsTrips
            .filter((rpt) => rpt.tripId === item.id)
            .map((rpt) => rpt.routePointId);

          // 2. Filter routePoints based on the IDs we found in the junction table
          const relatedRoutePoints = this.routePoints.filter((rp) =>
            relatedRoutePointIds.includes(rp.id)
          );

          this.tableData.push({
            trip: item,
            routePoints: relatedRoutePoints,
          });
        });

        console.log('After filter: ', this.tableData);
      });
  }

  deleteSelected() {
    const selectedIds: string[] = this.selection.selected.map(
      (item) => item.trip.id
    );

    console.log('Selected trip ids:', selectedIds);

    if (
      confirm(
        `Are you sure you want to delete ${selectedIds.length} selected trips?`
      )
    ) {
      this.tripService.deleteTrips(selectedIds).subscribe({
        next: (data) => {
          this.selection.clear();
          this.reloadSubject.next();
          this.successPopupService.show(
            `${selectedIds.length} trip(s) deleted successfully`
          );
        },
        error: (err) => {
          console.error(err);
        },
      });
    }
  }

  onRowClicked(tableData: TableData) {
    this.router.navigate(['..', 'trip'], {
      relativeTo: this.route,
      queryParams: {
        trip: JSON.stringify(tableData.trip),
        routePoints: JSON.stringify(tableData.routePoints),
      },
    });
  }

  toggleEditMode(): void {
    this.isEditMode = !this.isEditMode;

    // When exiting edit mode, clear selection
    if (!this.isEditMode) {
      this.selection.clear();
    }

    // Update displayed columns based on edit mode
    if (this.isEditMode) {
      if (!this.displayedColumns.includes('select')) {
        this.displayedColumns = ['select', ...this.displayedColumns];
      }
    } else {
      this.displayedColumns = this.displayedColumns.filter(
        (column) => column !== 'select'
      );
    }
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.tableData.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected()
      ? this.selection.clear()
      : this.tableData.forEach((row) => this.selection.select(row));
  }

  applyFilter(event: any) {
    const searchText = (event.target?.value || '').toLowerCase();

    // this.filteredLogisticians = this.logisticians.filter((logistician) => {
    //   const matchesSearch =
    //     `${logistician.firstName} ${logistician.lastName} ${logistician.email}`
    //       .toLowerCase()
    //       .includes(searchText);
    //   const matchesFilter =
    //     !this.selectedFilter ||
    //     (this.selectedFilter === 'admin' && logistician.hasAdminPrivileges) ||
    //     (this.selectedFilter === 'regular' && !logistician.hasAdminPrivileges);

    //   return matchesSearch && matchesFilter;
    // });
  }

  onAddTripClicked() {
    this.router.navigate(['trip-config']);
  }

  handlePageEvent(event: PageEvent) {
    this.pageSize = event.pageSize;
  }

  getContactName(contactInfo: ContactInfo): string {
    return contactInfo.firstName + ' ' + contactInfo.lastName;
  }

  getAddress(routePoints: RoutePoint[], type: string): string {
    let result: string = '';

    routePoints.forEach((rp) => {
      if (rp.type.valueOf() === type) {
        result += `${rp.sequence}) ${rp.address.city}, ${rp.address.street}, ${rp.address.number}\n`;
      }
    });
    return result.trimEnd();
  }

  getLicensePlate(truck: Transport, trailer: Transport): string {
    return truck.licensePlate + '\n' + trailer.licensePlate;
  }

  calculateProfit(trip: Trip): number {
    if (!trip.withTax) {
      return trip.customerPrice - trip.carrierPrice * 1.2;
    } else {
      return trip.customerPrice - trip.carrierPrice;
    }
  }
}
