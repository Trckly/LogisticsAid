import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-main-page',
  imports: [
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    RouterOutlet,
  ],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss',
})
export class MainPageComponent {
  showFiller: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute) {}

  onLogisticiansClicked() {
    this.router.navigate(['Logistician', 'logisticians']);
  }

  onTripsClicked() {
    this.router.navigate(['Logistician', 'trips']);
  }
}
