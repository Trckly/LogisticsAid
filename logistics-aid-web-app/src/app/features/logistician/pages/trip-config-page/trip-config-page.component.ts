import { Component, HostListener, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, NgForm } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

import { MatDividerModule } from '@angular/material/divider';
import { AuthService } from '../../../../core/auth/auth.service';
import { Router } from '@angular/router';
import { provideNativeDateAdapter } from '@angular/material/core';
import { Trip } from '../../../../shared/models/trip.model';
import { RoutePoint } from '../../../../shared/models/route-point.model';
import { ERoutePointType } from '../../../../shared/enums/route-point-type';

@Component({
  selector: 'app-trip-config-page',
  imports: [
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    FormsModule,
    MatDatepickerModule,
    MatIconModule,
    MatSlideToggleModule,
    MatCardModule,
    MatGridListModule,
    MatDividerModule,
  ],
  templateUrl: './trip-config-page.component.html',
  styleUrl: './trip-config-page.component.scss',
  providers: [provideNativeDateAdapter()],
})
export class TripConfigPageComponent implements OnInit {
  trip: Trip = new Trip();

  routePoints: RoutePoint[] = [];

  // UI
  gridCols: number = 2;

  constructor(private authService: AuthService, private router: Router) {}

  @HostListener('window:resize', ['$event'])
  onResize(event?: any) {
    const screenWidth = window.innerWidth;

    if (screenWidth > 1200) {
      this.gridCols = 3; // More columns on large screens
    } else if (screenWidth > 768) {
      this.gridCols = 2; // Two columns for medium screens
    } else {
      this.gridCols = 1; // Single column on small screens
    }
  }

  ngOnInit(): void {
    this.onResize();

    const initialLoadingPoint: RoutePoint = new RoutePoint();
    initialLoadingPoint.sequence = 1;
    initialLoadingPoint.trip = this.trip;
    initialLoadingPoint.type = ERoutePointType.Loading;

    const initialUnloadingPoint: RoutePoint = new RoutePoint();
    initialUnloadingPoint.sequence = 1;
    initialUnloadingPoint.trip = this.trip;
    initialUnloadingPoint.type = ERoutePointType.Unloading;

    this.routePoints.push(initialLoadingPoint, initialUnloadingPoint);
  }

  onSubmit(form: NgForm) {
    this.authService.formSubmitted = true;
    if (form.valid) {
    }
  }

  onCancel() {
    this.router.navigate(['..']);
  }

  isLoadingPoint(type: ERoutePointType): boolean {
    return type === ERoutePointType.Loading;
  }

  loadingPointsCount(): number {
    return this.routePoints.filter((rp) => rp.type === ERoutePointType.Loading)
      .length;
  }

  unloadingPointsCount(): number {
    return this.routePoints.filter(
      (rp) => rp.type === ERoutePointType.Unloading
    ).length;
  }

  addLoadingPoint() {
    const newLoadingPoint: RoutePoint = new RoutePoint();
    newLoadingPoint.sequence = this.loadingPointsCount() + 1;
    newLoadingPoint.trip = this.trip;
    newLoadingPoint.type = ERoutePointType.Loading;

    this.routePoints.push(newLoadingPoint);
  }

  addUnloadingPoint() {
    const newUnloadingPoint: RoutePoint = new RoutePoint();
    newUnloadingPoint.sequence = this.unloadingPointsCount() + 1;
    newUnloadingPoint.trip = this.trip;
    newUnloadingPoint.type = ERoutePointType.Unloading;

    this.routePoints.push(newUnloadingPoint);
  }

  removePoint(routePointToRemove: RoutePoint) {
    let type: ERoutePointType = routePointToRemove.type;

    const index = this.routePoints.indexOf(routePointToRemove);
    if (index !== -1) {
      this.routePoints.splice(index, 1);
    }

    let sequenceCounter = 0;
    for (let i = 0; i < this.routePoints.length; i++) {
      if (this.routePoints[i].type === type) {
        this.routePoints[i].sequence = ++sequenceCounter;
      }
    }
  }
}
