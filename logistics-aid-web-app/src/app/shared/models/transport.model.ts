import { ETransportType } from '../enums/transport-types';
import { CarrierCompany } from './carrier-company.model';

export class Transport {
  licensePlate: string;
  transportType: ETransportType;
  brand?: string;
  carrierCompany: CarrierCompany = new CarrierCompany();
}
