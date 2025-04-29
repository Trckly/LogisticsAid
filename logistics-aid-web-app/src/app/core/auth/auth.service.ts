import { Injectable, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Logistician } from '../../shared/models/logistician.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Login } from './login.model';
import { ContactInfo } from '../../shared/models/contact-info.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService implements OnInit {
  public url: string = environment.apiBaseUrl + '/User';
  public formData: Logistician = new Logistician();
  public formSubmitted = false;
  public username = 'Username';
  public isAdmin: boolean = false;

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

  updateUser(user: Logistician) {
    return this.http.put(this.url + '/UpdateUser', user, {
      withCredentials: true,
    });
  }

  login() {
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
            console.error('Logout error: ', err);
            reject(err);
          },
        });
    });
  }

  getUserWithToken(): Promise<Logistician | null> {
    return new Promise((resolve) => {
      this.http
        .get(this.url + '/GetUser', { withCredentials: true })
        .subscribe({
          next: (data) => {
            resolve(data as Logistician);
          },
          error: (err) => {
            console.error('Error getting user with token:', err.status);
            if (err.status === 401 && sessionStorage.getItem('user') != null) {
              sessionStorage.removeItem('user');
              // Don't call retrieveUsername here
              this.username = 'Username'; // Set directly instead
            }
            resolve(null);
          },
        });
    });
  }

  retrieveUsername() {
    this.getUserWithToken().then((currentUser) => {
      if (currentUser === null) {
        this.username = 'Username';
        console.error('Failed to get user from token');
      } else {
        this.username =
          currentUser.contactInfo.firstName +
          ' ' +
          currentUser.contactInfo.lastName;
      }
    });
  }

  retrieveAdminPrivileges(): Promise<boolean> {
    return this.getUserWithToken().then((currentUser) => {
      if (currentUser === null) {
        console.error('failed to get user from token');
        this.isAdmin = false;
        return false;
      } else {
        this.isAdmin = !!currentUser.hasAdminPrivileges;
        return this.isAdmin;
      }
    });
  }
}
