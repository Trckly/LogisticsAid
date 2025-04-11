import { Component, Input, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';

import { Trip } from '../../../../shared/models/trip.model';
import { ActivatedRoute, Router } from '@angular/router';
import { RoutePoint } from '../../../../shared/models/route-point.model';

@Component({
  selector: 'app-trip',
  imports: [MatCardModule, MatProgressSpinnerModule, MatPaginatorModule],
  templateUrl: './trip.component.html',
  styleUrl: './trip.component.scss',
})
export class TripComponent implements OnInit {
  trip: Trip = new Trip();
  routePoints: RoutePoint[] = [];

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (params['trip']) {
        this.trip = JSON.parse(params['trip']);
      }
      if (params['routePoints']) {
        this.routePoints = JSON.parse(params['routePoints']);
      }

      console.log('Trip:', this.trip);
      console.log('Route points:', this.routePoints);
    });
  }
}
