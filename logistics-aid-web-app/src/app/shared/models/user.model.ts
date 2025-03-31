import { ContactInfo } from './contact-info.model';

export class User {
  contactInfo: ContactInfo = new ContactInfo();

  password: string;
  hasAdminPrivileges: boolean;
}
