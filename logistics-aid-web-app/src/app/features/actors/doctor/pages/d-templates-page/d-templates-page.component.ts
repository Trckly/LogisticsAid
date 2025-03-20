import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { Questionnaire } from 'fhir/r5';
import { QuestionnaireService } from '../../../../questionnaire/questionaire.service';
import { User } from '../../../../../core/auth/user.model';
import { DQuestionnaireComponent } from '../../components/d-questionnaire/d-questionnaire.component';

@Component({
  selector: 'app-d-templates-page',
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatIconModule,
    MatSelectModule,
    FormsModule,
    MatCheckboxModule,
    MatDividerModule,
    MatTooltipModule,
    MatMenuModule,
    MatSidenavModule,
    MatListModule,
    RouterModule,
    DQuestionnaireComponent,
  ],
  templateUrl: './d-templates-page.component.html',
  styleUrl: './d-templates-page.component.scss',
})
export class DTemplatesPageComponent implements OnInit {
  templates: Questionnaire[] = [];

  constructor(
    private constructorService: QuestionnaireService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.retrieveDoctorTemplates();
  }

  onCreateClick() {
    this.router.navigate(['Doctor', 'constructor'], {
      queryParams: { isTemplate: true, patientEmail: null },
    });
  }

  refreshQuestionnaireList() {
    this.retrieveDoctorTemplates();
  }

  retrieveDoctorTemplates() {
    this.templates.splice(0, this.templates.length);

    const user: User = JSON.parse(sessionStorage.getItem('user')!);
    if (!user) {
      console.log('User is invalid!');
    }

    this.constructorService.getDoctorTemplates(user.email).subscribe({
      next: (data) => {
        if (Array.isArray(data)) {
          data.forEach((d) => {
            const questionnaire: Questionnaire = JSON.parse(d);
            if (questionnaire.status === 'draft')
              this.templates.push(JSON.parse(d));
          });
        }

        console.log(this.templates);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
