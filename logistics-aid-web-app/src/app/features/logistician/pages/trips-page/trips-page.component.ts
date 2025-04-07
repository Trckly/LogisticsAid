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
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
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

  selectedFilter: string = '';

  displayedColumns: string[] = ['ID', 'loading', 'unloading', 'price'];

  resultsLength = 0;
  isLoadingResults = true;
  isRateLimitReached = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private router: Router,
    private tripService: TripService,
    private errorPopupService: ErrorPopupService
  ) {}

  ngOnInit(): void {
    this.tripService.getAllTrips().subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (err) => {
        console.log(err);
        this.errorPopupService.show(err.error.message);
      },
    });
  }

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
        map((data: PaginatedResponse<Trip> | null) => {
          // Flip flag to show that loading has finished.
          this.isLoadingResults = false;
          this.isRateLimitReached = data === null;

          if (data === null) {
            return [];
          }

          // Only refresh the result length if there is new data. In case of rate
          // limit errors, we do not want to reset the paginator to zero, as that
          // would prevent users from re-triggering requests.
          this.resultsLength = data.totalCount;
          return data.items;
        })
      )
      .subscribe((data) => {
        this.paginatedTrips.items = data;

        this.paginatedTrips.items.forEach((trip) => {
          trip.loadingDate = new Date(trip.loadingDate);
          trip.unloadingDate = new Date(trip.unloadingDate);
        });
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
}
