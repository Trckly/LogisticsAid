import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { User } from '../../shared/models/user.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Login } from './login.model';
import { ContactInfo } from '../../shared/models/contact-info.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnInit {
  public url: string = environment.apiBaseUrl + '/User';
  public formData: User = new User();
  public formSubmitted = false;
  public username = 'Username';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

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

  updateUser(user: User) {
    return this.http.put(this.url + '/UpdateUser', user, {
      withCredentials: true,
    });
  }

  login() {
    console.log(JSON.stringify(this.formData));

    const credentials: Login = {
      email: this.formData.contactInfo.email,
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
    this.getUserWithToken().then((currentUser) => {
      if (currentUser === null) {
        this.username = 'Username';
        console.log('failed to get user from token');
      } else {
        this.username =
          currentUser.contactInfo.firstName +
          ' ' +
          currentUser.contactInfo.lastName;
      }
    });
  }

  retrieveAdminPriviledges(): boolean | null {
    const userJson = sessionStorage.getItem('user');
    if (userJson != null) {
      const user: User = JSON.parse(userJson);
      return user.hasAdminPrivileges;
    }
    return null;
  }
}
