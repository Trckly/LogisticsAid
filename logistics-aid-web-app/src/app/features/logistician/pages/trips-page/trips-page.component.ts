import { Component, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trips-page',
  imports: [
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule,
  ],
  templateUrl: './trips-page.component.html',
  styleUrl: './trips-page.component.scss',
})
export class TripsPageComponent implements OnInit {
  trips: string[] = ['Trip 1', 'Trip 2'];

  constructor(private router: Router) {}

  ngOnInit(): void {}

  selectedFilter: string = '';

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
