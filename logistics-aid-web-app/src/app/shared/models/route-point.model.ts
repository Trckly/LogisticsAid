import { ERoutePointType } from '../enums/route-point-type';
import { Address } from './address.model';
import { ContactInfo } from './contact-info.model';
import { Trip } from './trip.model';

export class RoutePoint {
  trip: Trip = new Trip();
  address: Address = new Address();
  companyName: string;
  type: ERoutePointType;
  sequence: number;
  contactInfo?: ContactInfo = new ContactInfo();
}
