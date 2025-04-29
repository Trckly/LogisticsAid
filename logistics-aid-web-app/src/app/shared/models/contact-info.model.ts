import { CustomerCompany } from './customer-company.model';
import { CarrierCompany } from './carrier-company.model';

export class ContactInfo {
  id: string;
  firstName: string;
  lastName: string;
  phone: string;
  email: string;
  birthDate?: Date;
}
