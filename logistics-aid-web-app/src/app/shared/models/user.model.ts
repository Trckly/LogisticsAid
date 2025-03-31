import { ContactInfo } from './contact-info.model';

export class User {
  // --- ContactInfo model on a server ---
  contactInfo: ContactInfo;

  // --- Logistician model on a server ---
  password: string;
  hasAdminPrivileges: boolean;
}
