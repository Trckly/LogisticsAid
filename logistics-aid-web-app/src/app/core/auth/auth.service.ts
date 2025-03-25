import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { User } from './user.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Login } from './login.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public url: string = environment.apiBaseUrl + '/User';
  public formData: User = new User();
  public formSubmitted = false;
  public username = 'Username';

  constructor(private http: HttpClient) {}

  checkAuthenticated(): Promise<boolean> {
    return new Promise((resolve) => {
      this.http
        .get<{ isAuthenticated: boolean }>(this.url + '/IsAuthenticated', {
          withCredentials: true,
        })
        .subscribe({
          next: (data) => {
            resolve(data.isAuthenticated);
          },
          error: (err) => {
            if (err.status === 401 && sessionStorage.getItem('user') != null) {
              sessionStorage.removeItem('user');
              this.retrieveUsername();
            }
            resolve(false);
          },
        });
    });
  }

  register() {
    console.log(JSON.stringify(this.formData));
    return this.http.post(this.url + '/Register', this.formData, {
      withCredentials: true,
    });
  }

  updateUser() {
    return this.http.put(this.url + '/UpdateUser', this.formData, {
      withCredentials: true,
    });
  }

  login() {
    console.log(JSON.stringify(this.formData));

    const credentials: Login = {
      email: this.formData.email,
      password: this.formData.password,
    };

    return this.http.post(this.url + '/Login', credentials, {
      withCredentials: true,
    });
  }

  logout(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.http
        .delete(this.url + '/Logout', { withCredentials: true })
        .subscribe({
          next: () => {
            if (sessionStorage.getItem('user') != null) {
              sessionStorage.removeItem('user');
              this.retrieveUsername();
              resolve();
            }
          },
          error: (err) => {
            console.log('Logout error: ', err);
            reject(err);
          },
        });
    });
  }

  getUserWithToken(): Promise<User | null> {
    return new Promise((resolve) => {
      this.http
        .get(this.url + '/GetUser', { withCredentials: true })
        .subscribe({
          next: (data) => {
            sessionStorage.setItem('user', JSON.stringify(data));
            // resolve((data as User).userType);
            resolve(data as User);
          },
          error: (err) => {
            if (err.status === 401 && sessionStorage.getItem('user') != null) {
              sessionStorage.removeItem('user');
              this.retrieveUsername();
            }
            resolve(null);
            console.log(err);
          },
        });
    });
  }

  retrieveUsername() {
    const userJson = sessionStorage.getItem('user');
    if (userJson === null) {
      this.username = 'Username';
      console.log('failed to get user from session storage');
    } else {
      console.log('JSON: ' + userJson);

      const user: User = JSON.parse(userJson);
      console.log(user);

      this.username = user.firstName + ' ' + user.lastName;
    }
  }
}
