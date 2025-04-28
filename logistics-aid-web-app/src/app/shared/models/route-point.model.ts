import { ERoutePointType } from '../enums/route-point-type';
import { Address } from './address.model';
import { ContactInfo } from './contact-info.model';
import { Trip } from './trip.model';

export class RoutePoint {
  id: string;
  type: ERoutePointType;
  sequence: number;
  companyName?: string;
  additionalInfo?: string;
  address: Address = new Address();
  trips?: Trip[] = [];
}
