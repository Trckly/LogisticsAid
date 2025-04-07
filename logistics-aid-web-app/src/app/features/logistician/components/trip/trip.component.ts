import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatPaginatorModule } from '@angular/material/paginator';

import { Trip } from '../../../../shared/models/trip.model';

@Component({
  selector: 'app-trip',
  imports: [MatCardModule, MatProgressSpinnerModule, MatPaginatorModule],
  templateUrl: './trip.component.html',
  styleUrl: './trip.component.scss',
})
export class TripComponent {
  @Input({ required: true }) trip: Trip;
}
