import { CarrierCompany } from './carrier-company.model';
import { CustomerCompany } from './customer-company.model';
import { Driver } from './driver.model';
import { Logistician } from './logistician.model';
import { RoutePoint } from './route-point.model';
import { Transport } from './transport.model';

export class Trip {
  id: string;
  readableId: string;
  dateCreated: Date;
  loadingDate: Date;
  unloadingDate: Date;
  logistician: Logistician = new Logistician();
  carrierCompany: CarrierCompany = new CarrierCompany();
  customerCompany: CustomerCompany = new CustomerCompany();
  driver: Driver = new Driver();
  truck: Transport = new Transport();
  trailer: Transport = new Transport();
  customerPrice: number;
  carrierPrice: number;
  cargoName: string;
  cargoWeight: number;
  withTax: boolean;
  routePoints: RoutePoint[] = [];
}
