import { Component, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCard, MatCardContent, MatCardHeader } from '@angular/material/card';
import {
  MatFormFieldModule,
  MatLabel,
  MatSuffix,
} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButton } from '@angular/material/button';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../auth.service';
import {
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle,
} from '@angular/material/datepicker';
import { MatGridList, MatGridTile } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { provideNativeDateAdapter } from '@angular/material/core';
import { User } from '../../../../shared/models/user.model';
import { UserService } from '../../../../shared/services/user.service';

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
    MatCardHeader,
    MatGridList,
    MatGridTile,
    MatIconModule,
    MatSlideToggleModule,
  ],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent implements OnInit {
  readonly _currentDate = new Date();
  logistician: User;
  currentUser: User;
  isSelf: boolean;

  constructor(
    public authService: AuthService,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.logistician = JSON.parse(params['user']);
    });

    let correctDate = this.logistician.birthDate;
    this.logistician.birthDate = new Date(correctDate);

    this.userService.getCurrentUser().then((user) => {
      this.currentUser = user;
    });

    if (this.logistician === this.currentUser || this.logistician == null) {
      this.isSelf = true;
    } else {
      this.isSelf = false;
    }
  }

  onSubmit(form: NgForm) {
    this.authService.formSubmitted = true;
    if (form.valid) {
      this.authService.updateUser(this.logistician).subscribe({
        next: (data) => {
          this.router.navigate(['..']);
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }

  onCancelClicked() {
    this.router.navigate(['..']);
  }
}
