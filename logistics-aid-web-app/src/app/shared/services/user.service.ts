import { Injectable } from '@angular/core';
import { Logistician } from '../models/logistician.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../core/auth/auth.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  url: string = environment.apiBaseUrl + '/User';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private router: Router
  ) {}

  getAllLogisticians() {
    return this.http.get(this.url + '/GetUsers', { withCredentials: true });
  }

  deleteLogistician(userId: string) {
    return this.http.delete(this.url + '/Delete/' + userId, {
      withCredentials: true,
    });
  }

  async getCurrentUser(): Promise<Logistician> {
    const user = await this.authService.getUserWithToken();
    if (user) {
      return user;
    } else {
      this.router.navigate(['/login']);
      return null;
    }
  }
}
