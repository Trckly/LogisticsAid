import { ContactInfo } from './contact-info.model';

export class Driver {
  license: string;
  companyName: string;
  contact: ContactInfo = new ContactInfo();
}
