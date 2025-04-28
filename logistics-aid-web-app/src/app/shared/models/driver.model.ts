import { ContactInfo } from './contact-info.model';
import { CarrierCompany } from './carrier-company.model';

export class Driver {
  license: string;
  contactInfo: ContactInfo = new ContactInfo();
  carrierCompany: CarrierCompany = new CarrierCompany();
}
