import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCard, MatCardContent, MatCardHeader } from '@angular/material/card';
import {
  MatFormFieldModule,
  MatLabel,
  MatSuffix,
} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../auth.service';
import {
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle,
} from '@angular/material/datepicker';
import { MatOption, provideNativeDateAdapter } from '@angular/material/core';
import { MatSelect } from '@angular/material/select';
import { GenderEnum } from '../../../../shared/enums/gender-enum';
import { UserRoleEnum } from '../../../../shared/enums/user-role-enum';
import { NgForOf } from '@angular/common';
import { User } from '../../user.model';

@Component({
  selector: 'app-profile',
  providers: [provideNativeDateAdapter()],
  imports: [
    MatToolbarModule,
    MatCard,
    MatCardContent,
    MatFormFieldModule,
    MatLabel,
    MatInputModule,
    MatButton,
    FormsModule,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatSuffix,
    MatDatepickerModule,
    MatSelect,
    MatOption,
    MatCardHeader,
    NgForOf,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent {
  EGender = Object.entries(GenderEnum);
  EUserRole = Object.entries(UserRoleEnum);
  readonly _currentDate = new Date();

  constructor(public service: AuthService, private router: Router) {
    let userJson = sessionStorage.getItem('user');
    if (userJson) {
      let user: User = JSON.parse(userJson);

      service.formData.firstName = user.firstName;
      service.formData.lastName = user.lastName;
      service.formData.email = user.email;
      service.formData.birthDate = user.birthDate;
      service.formData.phone = user.phone;
      service.formData.hasAdminPrivileges = user.hasAdminPrivileges;
    } else {
      console.log('User not found');
    }
  }

  onSubmit(form: NgForm) {
    this.service.formSubmitted = true;
    if (form.valid) {
      this.service.updateUser().subscribe({
        next: (data) => {
          console.log(data);
          sessionStorage.setItem('user', JSON.stringify(data));
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }

  logout() {
    this.service
      .logout()
      .then(() => this.router.navigate(['/login']))
      .catch((err) => console.log('Logout error:', err));
  }
}
