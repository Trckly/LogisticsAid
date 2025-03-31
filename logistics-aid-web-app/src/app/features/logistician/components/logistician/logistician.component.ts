import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { User } from '../../../../shared/models/user.model';
import { UserService } from '../../../../shared/services/user.service';
import { UtilService } from '../../../../shared/services/util.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logistician',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatMenuModule,
  ],
  templateUrl: './logistician.component.html',
  styleUrl: './logistician.component.scss',
})
export class LogisticianComponent {
  @Input({ required: true }) currentUser: User;
  @Input({ required: true }) logistician: User;

  @Output() userDeleted = new EventEmitter();

  constructor(
    public utilService: UtilService,
    private userService: UserService,
    private router: Router
  ) {}

  onDeleteClicked(user: User) {
    this.userService.deleteLogistician(user.contactInfo.id).subscribe({
      next: (data) => {
        this.userDeleted.emit();
      },
      error: (err) => {
        console.error('Failed to delete user');
      },
    });
  }

  onEditClicked(user: User) {
    this.router.navigate(['/profile'], {
      queryParams: { user: JSON.stringify(user) },
    });
  }
}
