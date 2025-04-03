import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { Logistician } from '../../../../shared/models/logistician.model';
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
  @Input({ required: true }) currentUser: Logistician;
  @Input({ required: true }) logistician: Logistician;

  @Output() userDeleted = new EventEmitter();

  constructor(
    public utilService: UtilService,
    private userService: UserService,
    private router: Router
  ) {}

  onDeleteClicked(user: Logistician) {
    this.userService.deleteLogistician(user.contactInfo.id).subscribe({
      next: (data) => {
        this.userDeleted.emit();
      },
      error: (err) => {
        console.error('Failed to delete user');
      },
    });
  }

  onEditClicked(user: Logistician) {
    this.router.navigate(['/profile'], {
      queryParams: { user: JSON.stringify(user) },
    });
  }
}
