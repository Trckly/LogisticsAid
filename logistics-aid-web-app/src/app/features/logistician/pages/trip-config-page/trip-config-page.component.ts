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
import { FormsModule, NgForm } from '@angular/forms';
import {
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle,
} from '@angular/material/datepicker';
import { MatGridList, MatGridTile } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { AuthService } from '../../../../core/auth/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-trip-config-page',
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
  templateUrl: './trip-config-page.component.html',
  styleUrl: './trip-config-page.component.scss',
})
export class TripConfigPageComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    this.authService.formSubmitted = true;
    if (form.valid) {
    }
  }

  onCancel() {
    this.router.navigate(['..']);
  }
}
