import { Component, OnInit, signal } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCard, MatCardContent, MatCardHeader } from '@angular/material/card';
import {
  MatFormFieldModule,
  MatLabel,
  MatSuffix,
} from '@angular/material/form-field';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatInputModule } from '@angular/material/input';
import { MatAnchor, MatButton, MatIconButton } from '@angular/material/button';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { AuthService } from '../../auth.service';
import { MatGridList, MatGridTile } from '@angular/material/grid-list';
import {
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle,
} from '@angular/material/datepicker';
import { provideNativeDateAdapter } from '@angular/material/core';
import { GenderEnum } from '../../../../shared/enums/gender-enum';
import { UserRoleEnum } from '../../../../shared/enums/user-role-enum';
import { MatIcon } from '@angular/material/icon';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-register',
  providers: [provideNativeDateAdapter()],
  imports: [
    MatToolbarModule,
    MatCard,
    MatCardContent,
    MatFormFieldModule,
    MatLabel,
    MatInputModule,
    MatButton,
    MatAnchor,
    RouterLink,
    FormsModule,
    MatGridList,
    MatGridTile,
    MatDatepickerInput,
    MatDatepickerToggle,
    MatSuffix,
    MatDatepickerModule,
    MatCardHeader,
    MatIconButton,
    MatIcon,
    MatSlideToggleModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent implements OnInit {
  EGender = Object.entries(GenderEnum);
  EUserRole = Object.entries(UserRoleEnum);
  readonly _currentDate = new Date();
  hidePassword = signal(true);
  confirmPassword = '';
  errorMessage: string = '';

  constructor(
    public service: AuthService,
    private router: Router,
    public route: ActivatedRoute
  ) {
    service.formData.password = 'embryo';
    service.formData.contactInfo.firstName = 'Danylo';
    service.formData.contactInfo.lastName = 'Shlomiak';
    service.formData.contactInfo.email = 'danylo@example.com';
    service.formData.contactInfo.birthDate = new Date(2005, 2, 4);
    service.formData.contactInfo.phone = '+380676847525';
    service.formData.hasAdminPrivileges = true;
  }

  ngOnInit(): void {}

  changeButtonVisibility(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }

  isPasswordMatch(
    event: FocusEvent,
    password: NgModel,
    confirmPassword: NgModel
  ) {
    if (password.value != confirmPassword.value) {
      password.control.setErrors({ mismatch: true });
      confirmPassword.control.setErrors({ mismatch: true });
    } else {
      password.control.setErrors(null);
      confirmPassword.control.setErrors(null);
    }
    event.stopPropagation();
  }

  onSubmit(form: NgForm) {
    this.service.formSubmitted = true;
    if (form.valid) {
      this.service.formData.contactInfo.id = uuidv4();
      this.service.register().subscribe({
        next: (data) => {
          console.log(data);
          this.router.navigate([`/Logistician`]);
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }
}
