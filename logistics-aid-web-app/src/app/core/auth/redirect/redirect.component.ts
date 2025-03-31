import { Component, inject } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-redirect',
  imports: [],
  template: '',
  styles: '',
})
export class RedirectComponent {
  private authService = inject(AuthService);
  private router = inject(Router);
  private role: string | null;

  constructor() {
    this.authService.getUserWithToken().then((user) => {
      if (user != null) {
        console.log('Role was null but now dont');
        this.authService.retrieveUsername();
        this.router.navigate(['/' + 'Logistician']);
      } else {
        console.log('role was null and still null');
        this.router.navigate(['/login']);
      }
    });
  }

  // checkUserRole() {
  //   let userString = sessionStorage.getItem('user');
  //   if (userString === null) return null;
  //   return (JSON.parse(userString) as User).userType;
  // }
}
