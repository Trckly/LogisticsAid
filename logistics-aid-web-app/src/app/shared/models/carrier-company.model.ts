import { ContactInfo } from './contact-info.model';
import { Driver } from './driver.model';
import { Transport } from './transport.model';

export class CarrierCompany {
  companyName: string;

  contacts: ContactInfo[] = [];
  drivers: Driver[] = [];
  transport: Transport[] = [];
}
