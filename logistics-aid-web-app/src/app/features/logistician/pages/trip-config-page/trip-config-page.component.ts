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
import { Transport } from '../../../../shared/models/transport.model';

class RoutePointExtended {
  routePoint: RoutePoint = new RoutePoint();
  compositeAdress: string = '';
}

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

  routePointsExtended: RoutePointExtended[] = [];

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

    const initialLoadingPoint: RoutePointExtended = new RoutePointExtended();
    initialLoadingPoint.routePoint.sequence = 1;
    initialLoadingPoint.routePoint.trip = this.trip;
    initialLoadingPoint.routePoint.type = ERoutePointType.Loading;
    initialLoadingPoint.compositeAdress =
      'Україна, Львівська обл., м. Львів, вул. Наукова, 5а';

    const initialUnloadingPoint: RoutePointExtended = new RoutePointExtended();
    initialUnloadingPoint.routePoint.sequence = 1;
    initialUnloadingPoint.routePoint.trip = this.trip;
    initialUnloadingPoint.routePoint.type = ERoutePointType.Unloading;
    initialUnloadingPoint.compositeAdress =
      'Україна, Київська обл., м. Київ, вул. Хрещатик, 27';

    this.routePointsExtended.push(initialLoadingPoint, initialUnloadingPoint);

    this.trip.readableId = '1111p';
    this.trip.price = 25000;
    this.trip.cargoName = 'метал';

    this.trip.customer.contact.firstName = 'Андрій';
    this.trip.customer.contact.lastName = 'Юшкевич';
    this.trip.customer.contact.phone = '+380677653443';
    this.trip.customer.companyName = 'Андрій Метал';

    this.trip.carrier.contact.firstName = 'Володимир';
    this.trip.carrier.contact.lastName = 'Шурко';
    this.trip.carrier.contact.phone = '+380675676789';
    this.trip.carrier.companyName = 'Вова Транс';

    this.trip.driver.contact.firstName = 'Олег';
    this.trip.driver.contact.lastName = 'Джамбо';
    this.trip.driver.contact.phone = '+380670989898';
    this.trip.driver.license = 'BXI123456';

    this.trip.transport.licencePlate = 'AC4567BX';
    this.trip.transport.trailerLicencePlate = 'AC5543XM';
    this.trip.transport.truckBrand = 'MAN';
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
    return this.routePointsExtended.filter(
      (rpe) => rpe.routePoint.type === ERoutePointType.Loading
    ).length;
  }

  unloadingPointsCount(): number {
    return this.routePointsExtended.filter(
      (rpe) => rpe.routePoint.type === ERoutePointType.Unloading
    ).length;
  }

  addLoadingPoint() {
    const newLoadingPoint: RoutePointExtended = new RoutePointExtended();
    newLoadingPoint.routePoint = { ...new RoutePointExtended().routePoint }; // Ensure a new object
    newLoadingPoint.routePoint.sequence = this.loadingPointsCount() + 1;
    newLoadingPoint.routePoint.trip = this.trip;
    newLoadingPoint.routePoint.type = ERoutePointType.Loading;
    newLoadingPoint.compositeAdress = 'hfjdksagf';

    this.routePointsExtended = [...this.routePointsExtended, newLoadingPoint]; // Ensure immutability
  }

  addUnloadingPoint() {
    const newUnloadingPoint: RoutePointExtended = new RoutePointExtended();
    newUnloadingPoint.routePoint = { ...new RoutePointExtended().routePoint }; // Ensure a new object
    newUnloadingPoint.routePoint.sequence = this.unloadingPointsCount() + 1;
    newUnloadingPoint.routePoint.trip = this.trip;
    newUnloadingPoint.routePoint.type = ERoutePointType.Unloading;
    newUnloadingPoint.compositeAdress = 'hfguidsaolhgufi';

    this.routePointsExtended = [...this.routePointsExtended, newUnloadingPoint]; // Ensure immutability
  }

  removePoint(routePointToRemove: RoutePointExtended) {
    let type: ERoutePointType = routePointToRemove.routePoint.type;

    const index = this.routePointsExtended.indexOf(routePointToRemove);
    if (index !== -1) {
      this.routePointsExtended.splice(index, 1);
    }

    let sequenceCounter = 0;
    for (let i = 0; i < this.routePointsExtended.length; i++) {
      if (this.routePointsExtended[i].routePoint.type === type) {
        this.routePointsExtended[i].routePoint.sequence = ++sequenceCounter;
      }
    }
  }
}
