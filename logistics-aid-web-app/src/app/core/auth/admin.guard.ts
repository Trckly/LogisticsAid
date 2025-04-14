import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

export const adminGuard = async () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const hasAdminRole = await authService.retrieveAdminPrivileges();

  if (hasAdminRole) {
    return true;
  }

  return router.parseUrl('/Logistician');
};
