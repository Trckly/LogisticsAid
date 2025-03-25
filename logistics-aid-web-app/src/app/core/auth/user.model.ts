export class User {
  // --- ContactInfo model on a server ---
  id: string;
  firstName: string;
  lastName: string;
  phone: string;
  email?: string;
  birthDate?: Date;

  // --- Logistician model on a server ---
  password: string;
  hasAdminPrivileges: boolean;
}
