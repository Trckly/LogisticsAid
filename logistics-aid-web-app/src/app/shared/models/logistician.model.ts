import { ContactInfo } from './contact-info.model';

export class Logistician {
  contactInfo: ContactInfo = new ContactInfo();

  password: string;
  hasAdminPrivileges: boolean;
}
