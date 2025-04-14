import { Carrier } from './carrier.model';
import { Customer } from './customer.model';
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
  carrier: Carrier = new Carrier();
  customer: Customer = new Customer();
  driver: Driver = new Driver();
  transport: Transport = new Transport();
  customerPrice: number;
  carrierPrice: number;
  withTax: boolean;
  cargoName: string;
  cargoWeight: number;
  routePoints: RoutePoint[] = [];
}
