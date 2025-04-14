import { inject } from '@angular/core';
import {AuthService} from './auth.service';
import {Router} from '@angular/router';

export const authGuard = async () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const isAuthenticated = await authService.checkAuthenticated();
  console.log("Authentication check: ", isAuthenticated);

  if(isAuthenticated) {
    // Get the username separately after authentication is confirmed
    setTimeout(() => authService.retrieveUsername(), 0); // Defer this call
    return true;
  }
  return router.parseUrl('/login');
};
