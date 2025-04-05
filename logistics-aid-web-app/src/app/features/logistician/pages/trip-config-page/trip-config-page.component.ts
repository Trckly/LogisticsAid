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
import { Logistician } from '../../../../shared/models/logistician.model';
import { v4 as uuidv4 } from 'uuid';
import { TripService } from '../../../../shared/services/trip.service';
import { Address } from '../../../../shared/models/address.model';

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
  currentUser: Logistician = new Logistician();

  routePointsExtended: RoutePointExtended[] = [];

  // UI
  gridCols: number = 2;

  constructor(
    private authService: AuthService,
    private tripService: TripService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.getUserWithToken().then((user) => {
      this.currentUser = user;

      const initialLoadingPoint: RoutePointExtended = new RoutePointExtended();
      initialLoadingPoint.routePoint.id = uuidv4();
      initialLoadingPoint.routePoint.sequence = 1;
      initialLoadingPoint.routePoint.tripId = this.trip.id;
      initialLoadingPoint.routePoint.type = ERoutePointType.Loading;
      initialLoadingPoint.routePoint.companyName = 'Метінвест';
      initialLoadingPoint.compositeAdress =
        'Україна, Львівська обл., м. Львів, вул. Наукова, 5а';
      initialLoadingPoint.routePoint.contactInfo.id = uuidv4();
      initialLoadingPoint.routePoint.contactInfo.firstName = 'Джо';
      initialLoadingPoint.routePoint.contactInfo.lastName = 'Рандомний';
      initialLoadingPoint.routePoint.contactInfo.phone = '+380678754635';

      const initialUnloadingPoint: RoutePointExtended =
        new RoutePointExtended();
      initialUnloadingPoint.routePoint.id = uuidv4();
      initialUnloadingPoint.routePoint.sequence = 1;
      initialUnloadingPoint.routePoint.tripId = this.trip.id;
      initialUnloadingPoint.routePoint.type = ERoutePointType.Unloading;
      initialUnloadingPoint.routePoint.companyName = 'Інтергалбуд';
      initialUnloadingPoint.compositeAdress =
        'Україна, Київська обл., м. Київ, вул. Хрещатик, 27';
      initialUnloadingPoint.routePoint.contactInfo.id = uuidv4();
      initialUnloadingPoint.routePoint.contactInfo.firstName = 'Максим';
      initialUnloadingPoint.routePoint.contactInfo.lastName = 'Мельник';
      initialUnloadingPoint.routePoint.contactInfo.phone = '+380673542789';

      this.routePointsExtended.push(initialLoadingPoint, initialUnloadingPoint);

      this.trip.readableId = '1111p';
      this.trip.price = 25000;
      this.trip.cargoName = 'метал';

      this.trip.customer.contact.id = uuidv4();
      this.trip.customer.contact.firstName = 'Андрій';
      this.trip.customer.contact.lastName = 'Юшкевич';
      this.trip.customer.contact.phone = '+380677653443';
      this.trip.customer.companyName = 'Андрій Метал';

      this.trip.carrier.contact.id = uuidv4();
      this.trip.carrier.contact.firstName = 'Володимир';
      this.trip.carrier.contact.lastName = 'Шурко';
      this.trip.carrier.contact.phone = '+380675676789';
      this.trip.carrier.companyName = 'Вова Транс';

      this.trip.driver.contact.id = uuidv4();
      this.trip.driver.contact.firstName = 'Олег';
      this.trip.driver.contact.lastName = 'Джамбо';
      this.trip.driver.contact.phone = '+380670989898';
      this.trip.driver.license = 'BXI123456';

      this.trip.transport.licencePlate = 'AC4567BX';
      this.trip.transport.trailerLicencePlate = 'AC5543XM';
      this.trip.transport.truckBrand = 'MAN';
    });
  }

  onSubmit(form: NgForm) {
    this.authService.formSubmitted = true;
    if (form.valid) {
      this.trip.id = uuidv4();
      this.trip.dateCreated = new Date();
      this.trip.driver.companyName = this.trip.carrier.companyName;
      this.trip.transport.companyName = this.trip.carrier.companyName;
      this.trip.logistician = this.currentUser;
      this.organizeRoutePoints();
      this.trip.routePoints = this.routePointsExtended.map(
        (rpe) => rpe.routePoint
      );

      this.tripService.addTrip(this.trip).subscribe({
        next: (data) => {
          console.log(data);
        },
        error: (err) => {
          console.error(err);
        },
      });
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
    newLoadingPoint.routePoint = { ...new RoutePointExtended().routePoint };
    newLoadingPoint.routePoint.sequence = this.loadingPointsCount() + 1;
    newLoadingPoint.routePoint.tripId = this.trip.id;
    newLoadingPoint.routePoint.type = ERoutePointType.Loading;

    this.routePointsExtended = [...this.routePointsExtended, newLoadingPoint];
  }

  addUnloadingPoint() {
    const newUnloadingPoint: RoutePointExtended = new RoutePointExtended();
    newUnloadingPoint.routePoint = { ...new RoutePointExtended().routePoint };
    newUnloadingPoint.routePoint.sequence = this.unloadingPointsCount() + 1;
    newUnloadingPoint.routePoint.tripId = this.trip.id;
    newUnloadingPoint.routePoint.type = ERoutePointType.Unloading;

    this.routePointsExtended = [...this.routePointsExtended, newUnloadingPoint];
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

  organizeRoutePoints() {
    this.routePointsExtended.forEach((rpe) => {
      rpe.routePoint.address = this.parseAddress(rpe.compositeAdress);
      rpe.routePoint.tripId = this.trip.id;
    });
  }

  parseAddress(compositeAddress: string): Address | null {
    const regex =
      /^([^,]+),\s*([^,]+ обл\.)?,?\s*(м\.|с\.|смт\.|місто)?\s*([^,]+)?,?\s*(вул\.|просп\.|пров\.|пл\.)?\s*([^,]+)?,?\s*(\d+\S*)?/;
    const match = compositeAddress.match(regex);

    if (!match) return null;

    let address = new Address();
    address.id = uuidv4();
    address.country = match[1]?.trim() || '';
    address.province = match[2]?.replace(' обл.', '').trim() || '';
    address.city = match[4]?.trim() || '';
    address.street = match[6]?.trim() || '';
    address.number = match[7] || '';

    return address;
  }
}
