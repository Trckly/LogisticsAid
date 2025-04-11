import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import {
  MatPaginator,
  MatPaginatorModule,
  PageEvent,
} from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { Trip } from '../../../../shared/models/trip.model';
import { TripService } from '../../../../shared/services/trip.service';
import { ErrorPopupService } from '../../../../shared/services/error-popup.service';
import {
  merge,
  startWith,
  switchMap,
  catchError,
  map,
  Observable,
  of as observableOf,
} from 'rxjs';
import { PaginatedResponse } from '../../../../shared/models/paginated-response.model';
import { RoutePoint } from '../../../../shared/models/route-point.model';
import { ContactInfo } from '../../../../shared/models/contact-info.model';
import { Address } from '../../../../shared/models/address.model';
import { ERoutePointType } from '../../../../shared/enums/route-point-type';
import { Transport } from '../../../../shared/models/transport.model';

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
  ],
  templateUrl: './trips-page.component.html',
  styleUrl: './trips-page.component.scss',
  providers: [DatePipe],
})
export class TripsPageComponent implements OnInit, AfterViewInit {
  paginatedTrips: PaginatedResponse<Trip> = new PaginatedResponse<Trip>();
  routePoints: RoutePoint[] = [];

  tableData: TableData[] = [];

  selectedFilter: string = '';

  displayedColumns: string[] = [
    'ID',
    'loadingDate',
    'unloadingDate',
    'price',
    'loadingAddress',
    'unloadingAddress',
    'driverName',
    'licensePlate',
  ];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  pageSize: number = 10;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private router: Router,
    private tripService: TripService,
    private errorPopupService: ErrorPopupService
  ) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    // If the user changes the sort order, reset back to the first page.
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    merge(this.sort.sortChange, this.paginator.page)
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
        map((result) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;

          const { trips, routePoints } = result;
          this.isRateLimitReached = trips === null;

          if (trips === null) {
            return { tripItems: [], routePointItems: [] };
          }

          // Update results length for pagination
          this.resultsLength = trips.totalCount;

          return {
            tripItems: trips.items,
            routePointItems: routePoints,
          };
        })
      )
      .subscribe((data) => {
        this.paginatedTrips.items = data.tripItems;
        this.routePoints = data.routePointItems;

        this.tableData = [];

        this.paginatedTrips.items.forEach((trip) => {
          trip.loadingDate = new Date(trip.loadingDate);
          trip.unloadingDate = new Date(trip.unloadingDate);
        });

        this.paginatedTrips.items.forEach((item) => {
          this.tableData.push({
            trip: item,
            routePoints: this.routePoints.filter((rp) => rp.tripId === item.id),
          });
        });

        console.log(this.tableData);
      });
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
        result += `${rp.address.city}, ${rp.address.street}, ${rp.address.number}\n`;
      }
    });
    return result.trimEnd();
  }

  getLicensePlate(transport: Transport): string {
    return transport.licensePlate + '\n' + transport.trailerLicensePlate;
  }
}
