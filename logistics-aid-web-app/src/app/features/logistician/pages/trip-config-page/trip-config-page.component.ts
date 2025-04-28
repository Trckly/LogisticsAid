import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import {
  FormsModule,
  NgForm,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDividerModule } from '@angular/material/divider';
import { MatStepperModule } from '@angular/material/stepper';

import { AuthService } from '../../../../core/auth/auth.service';
import { Router } from '@angular/router';
import {
  MAT_DATE_LOCALE,
  provideNativeDateAdapter,
} from '@angular/material/core';
import { Trip } from '../../../../shared/models/trip.model';
import { RoutePoint } from '../../../../shared/models/route-point.model';
import { ERoutePointType } from '../../../../shared/enums/route-point-type';
import { Logistician } from '../../../../shared/models/logistician.model';
import { v4 as uuidv4 } from 'uuid';
import { TripService } from '../../../../shared/services/trip.service';
import { Address } from '../../../../shared/models/address.model';
import { ErrorPopupService } from '../../../../shared/services/error-popup.service';
import { SuccessPopupService } from '../../../../shared/services/success-popup.service';
import { ContactInfo } from '../../../../shared/models/contact-info.model';
import { UtilService } from '../../../../shared/services/util.service';
import { CustomerCompany } from '../../../../shared/models/customer-company.model';
import { CarrierCompany } from '../../../../shared/models/carrier-company.model';
import { ETransportType } from '../../../../shared/enums/transport-types';

class RoutePointExtended {
  routePoint: RoutePoint = new RoutePoint();
  compositeAddress: string = '';
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
    MatButtonToggleModule,
    MatStepperModule,
  ],
  templateUrl: './trip-config-page.component.html',
  styleUrl: './trip-config-page.component.scss',
  providers: [
    provideNativeDateAdapter(),
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' },
  ],
})
export class TripConfigPageComponent implements OnInit {
  trip: Trip = new Trip();
  currentUser: Logistician = new Logistician();

  routePointsExtended: RoutePointExtended[] = [];

  minDate: Date = new Date();

  // Add form groups for stepper
  tripDetailsFormGroup: FormGroup;
  customerFormGroup: FormGroup;
  carrierFormGroup: FormGroup;
  loadingFormGroup: FormGroup;
  unloadingFormGroup: FormGroup;
  driverVehicleFormGroup: FormGroup;

  // ViewChild for the stepper
  @ViewChild('stepper') stepper;

  constructor(
    private authService: AuthService,
    private tripService: TripService,
    private utilService: UtilService,
    private router: Router,
    private errorPopupService: ErrorPopupService,
    private SuccessPopupService: SuccessPopupService,
    private _formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    // Initialize form groups with validators
    this.tripDetailsFormGroup = this._formBuilder.group({
      readableId: ['', Validators.required],
      customerPrice: ['', Validators.required],
      carrierPrice: ['', Validators.required],
      cargoName: ['', Validators.required],
      cargoWeight: ['', Validators.required],
      withTax: [false],
    });

    this.customerFormGroup = this._formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phone: ['', Validators.required],
      companyName: ['', Validators.required],
    });

    this.carrierFormGroup = this._formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phone: ['', Validators.required],
      companyName: ['', Validators.required],
    });

    this.loadingFormGroup = this._formBuilder.group({
      loadingDate: ['', Validators.required],
      // For dynamic loading points, you can add form arrays if needed
    });

    this.unloadingFormGroup = this._formBuilder.group({
      unloadingDate: ['', Validators.required],
      // For dynamic unloading points, you can add form arrays if needed
    });

    this.driverVehicleFormGroup = this._formBuilder.group({
      driverFirstName: ['', Validators.required],
      driverLastName: ['', Validators.required],
      driverPhone: ['', Validators.required],
      driverLicense: ['', Validators.required],
      vehicleModel: ['', Validators.required],
      vehicleLicensePlate: ['', Validators.required],
      trailerLicensePlate: ['', Validators.required],
    });

    this.authService.getUserWithToken().then((user) => {
      this.currentUser = user;

      this.minDate = new Date();
      this.minDate.setHours(0, 0, 0, 0);

      const initialLoadingPoint: RoutePointExtended = new RoutePointExtended();
      initialLoadingPoint.routePoint.id = uuidv4();
      initialLoadingPoint.routePoint.sequence = 1;
      initialLoadingPoint.routePoint.type = ERoutePointType.Loading;
      initialLoadingPoint.routePoint.companyName = 'Метінвест';
      initialLoadingPoint.compositeAddress =
        'Україна, Львівська обл., м. Львів, вул. Наукова, 5а';
      initialLoadingPoint.routePoint.additionalInfo =
        'Джо Рандомний +380678765634';

      const initialLoadingPoint2: RoutePointExtended = new RoutePointExtended();
      initialLoadingPoint2.routePoint.id = uuidv4();
      initialLoadingPoint2.routePoint.sequence = 2;
      initialLoadingPoint2.routePoint.type = ERoutePointType.Loading;
      initialLoadingPoint2.routePoint.companyName = 'Метінвест-СМЦ';
      initialLoadingPoint2.compositeAddress =
        'Україна, Львівська обл., м. Миколаїв, вул. Окружна, 12';
      initialLoadingPoint2.routePoint.additionalInfo =
        'Павло Петращук +380987770044';
      const initialUnloadingPoint: RoutePointExtended =
        new RoutePointExtended();
      initialUnloadingPoint.routePoint.id = uuidv4();
      initialUnloadingPoint.routePoint.sequence = 1;
      initialUnloadingPoint.routePoint.type = ERoutePointType.Unloading;
      initialUnloadingPoint.routePoint.companyName = 'Інтергалбуд';
      initialUnloadingPoint.compositeAddress =
        'Україна, Київська обл., м. Київ, вул. Хрещатик, 27';
      initialUnloadingPoint.routePoint.additionalInfo =
        'Максим Мельник +380673542789';

      const initialUnloadingPoint2: RoutePointExtended =
        new RoutePointExtended();
      initialUnloadingPoint2.routePoint.id = uuidv4();
      initialUnloadingPoint2.routePoint.sequence = 2;
      initialUnloadingPoint2.routePoint.type = ERoutePointType.Unloading;
      initialUnloadingPoint2.routePoint.companyName = 'Оболоньсталь';
      initialUnloadingPoint2.compositeAddress =
        'Україна, Київська обл., м. Оболонь, вул. Надвірна, 2';
      initialUnloadingPoint2.routePoint.additionalInfo =
        'Матвій Первак +380970003322';

      this.routePointsExtended.push(
        initialLoadingPoint,
        initialLoadingPoint2,
        initialUnloadingPoint,
        initialUnloadingPoint2
      );

      this.trip.readableId = '1111p';
      this.trip.customerPrice = 30000;
      this.trip.carrierPrice = 25000;
      this.trip.withTax = true;
      this.trip.cargoName = 'метал';
      this.trip.cargoWeight = 22;

      let customerCompany = new CustomerCompany();
      customerCompany.companyName = 'Андрій Метал';

      let carrierCompany = new CarrierCompany();
      carrierCompany.companyName = 'Вова Транс';

      this.trip.driver.contactInfo = new ContactInfo();
      this.trip.driver.contactInfo.id = uuidv4();
      this.trip.driver.contactInfo.firstName = 'Олег';
      this.trip.driver.contactInfo.lastName = 'Джамбо';
      this.trip.driver.contactInfo.phone = '+380670989898';
      this.trip.driver.license = 'BXI123456';

      this.trip.truck.licensePlate = 'AC4567BX';
      this.trip.truck.transportType = ETransportType.Truck;
      this.trip.truck.brand = 'MAN';
      this.trip.truck.carrierCompany = carrierCompany;

      this.trip.trailer.licensePlate = 'AC5543XM';
      this.trip.trailer.transportType = ETransportType.Trailer;
      this.trip.trailer.brand = 'Cargobull';
      this.trip.trailer.carrierCompany = carrierCompany;

      // Update form values from trip data
      this.updateFormGroupsFromTripData();
    });
  }

  // Add method to update form groups from trip data
  updateFormGroupsFromTripData(): void {
    // Update trip details form
    this.tripDetailsFormGroup.patchValue({
      readableId: this.trip.readableId,
      customerPrice: this.trip.customerPrice,
      carrierPrice: this.trip.carrierPrice,
      cargoName: this.trip.cargoName,
      cargoWeight: this.trip.cargoWeight,
    });

    // Update customer form
    this.customerFormGroup.patchValue({
      companyName: this.trip.customerCompany.companyName,
    });

    // Update carrier form
    this.carrierFormGroup.patchValue({
      companyName: this.trip.carrierCompany.companyName,
    });

    // Update driver & vehicle form
    this.driverVehicleFormGroup.patchValue({
      driverFirstName: this.trip.driver.contactInfo.firstName,
      driverLastName: this.trip.driver.contactInfo.lastName,
      driverPhone: this.trip.driver.contactInfo.phone,
      driverLicense: this.trip.driver.license,

      truckModel: this.trip.truck.brand,
      truckLicensePlate: this.trip.truck.licensePlate,

      trailerModel: this.trip.trailer.brand,
      trailerLicensePlate: this.trip.trailer.licensePlate,
    });
  }

  onSubmit(form: NgForm) {
    this.authService.formSubmitted = true;
    if (form.valid) {
      this.trip.id = uuidv4();
      this.trip.dateCreated = new Date();
      this.trip.driver.carrierCompany = this.trip.carrierCompany;
      this.trip.truck.carrierCompany = this.trip.carrierCompany;
      this.trip.trailer.carrierCompany = this.trip.carrierCompany;
      this.trip.logistician = this.currentUser;
      this.organizeRoutePoints();
      this.trip.routePoints = this.routePointsExtended.map(
        (rpe) => rpe.routePoint
      );

      this.tripService.addTrip(this.trip).subscribe({
        next: (data) => {
          console.log(data);
          this.SuccessPopupService.show('Trip saved successfully');
          this.router.navigate(['Logistician/trips']);
        },
        error: (err) => {
          this.errorPopupService.show(err?.error?.message);
          console.log(this.routePointsExtended);
        },
      });
    } else {
      this.errorPopupService.show('Not all mandatory fields are filled!');
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
    newLoadingPoint.routePoint.id = uuidv4();
    newLoadingPoint.routePoint.sequence = this.loadingPointsCount() + 1;
    newLoadingPoint.routePoint.type = ERoutePointType.Loading;
    newLoadingPoint.routePoint.additionalInfo = '';

    this.routePointsExtended = [...this.routePointsExtended, newLoadingPoint];
  }

  addUnloadingPoint() {
    const newUnloadingPoint: RoutePointExtended = new RoutePointExtended();
    newUnloadingPoint.routePoint.id = uuidv4();
    newUnloadingPoint.routePoint = { ...new RoutePointExtended().routePoint };
    newUnloadingPoint.routePoint.sequence = this.unloadingPointsCount() + 1;
    newUnloadingPoint.routePoint.type = ERoutePointType.Unloading;
    newUnloadingPoint.routePoint.additionalInfo = '';

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
      rpe.routePoint.address = this.parseAddress(rpe.compositeAddress);
      rpe.routePoint.trips.filter((trip) => trip.id === this.trip.id);
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

  getReadableDate(date: any): string {
    return this.utilService.getReadableDate(date);
  }
}
