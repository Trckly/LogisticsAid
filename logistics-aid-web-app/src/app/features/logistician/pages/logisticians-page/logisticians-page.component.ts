import { Component, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { User } from '../../../../shared/models/user.model';
import { UserService } from '../../../../shared/services/user.service';
import { UtilService } from '../../../../shared/services/util.service';
import { Router } from '@angular/router';
import { LogisticianComponent } from '../../components/logistician/logistician.component';

@Component({
  selector: 'app-logisticians-page',
  imports: [
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatCardModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatListModule,
    MatMenuModule,
    LogisticianComponent,
  ],
  templateUrl: './logisticians-page.component.html',
  styleUrls: ['./logisticians-page.component.scss'],
})
export class LogisticiansPageComponent implements OnInit {
  constructor(
    private userService: UserService,
    private utilService: UtilService,
    private router: Router
  ) {}

  currentUser: User;
  logisticians: User[] = [];
  filteredLogisticians: User[] = [];
  selectedFilter: string = '';

  ngOnInit(): void {
    this.retrieveAllUsers();

    this.userService.getCurrentUser().then((user) => {
      this.currentUser = user;
    });
  }

  retrieveAllUsers() {
    this.logisticians = [];
    this.filteredLogisticians = [];

    this.userService.getAllLogisticians().subscribe({
      next: (data) => {
        if (Array.isArray(data)) {
          data.forEach((user) => {
            this.logisticians.push(user);
          });
        }

        this.filteredLogisticians = [...this.logisticians];
      },
      error: (err) => {
        console.error('Failed to retrieve users');
      },
    });
  }

  applyFilter(event: any) {
    const searchText = (event.target?.value || '').toLowerCase();

    this.filteredLogisticians = this.logisticians.filter((logistician) => {
      const matchesSearch =
        `${logistician.firstName} ${logistician.lastName} ${logistician.email}`
          .toLowerCase()
          .includes(searchText);
      const matchesFilter =
        !this.selectedFilter ||
        (this.selectedFilter === 'admin' && logistician.hasAdminPrivileges) ||
        (this.selectedFilter === 'regular' && !logistician.hasAdminPrivileges);

      return matchesSearch && matchesFilter;
    });
  }

  onUserDeleted() {
    this.retrieveAllUsers();
  }
}
