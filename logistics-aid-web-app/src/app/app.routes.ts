import { Routes } from '@angular/router';
import { RegisterComponent } from './core/auth/pages/register/register.component';
import { LoginComponent } from './core/auth/pages/login/login.component';
import { authGuard } from './core/auth/auth.guard';
import { RedirectComponent } from './core/auth/redirect/redirect.component';
import { ProfileComponent } from './core/auth/pages/profile/profile.component';
import { MainPageComponent } from './features/logistician/pages/main-page/main-page.component';
import { LogisticiansPageComponent } from './features/logistician/pages/logisticians-page/logisticians-page.component';
import { TripsPageComponent } from './features/logistician/pages/trips-page/trips-page.component';
import { TripConfigPageComponent } from './features/logistician/pages/trip-config-page/trip-config-page.component';
import { TripComponent } from './features/logistician/components/trip/trip.component';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'redirect',
    pathMatch: 'full',
  },
  {
    path: 'redirect',
    component: RedirectComponent,
    title: 'Redirect Page',
  },
  {
    path: 'Logistician',
    component: MainPageComponent,
    canActivate: [authGuard],
    title: 'Main Page',
    children: [
      {
        path: '',
        redirectTo: 'logisticians',
        pathMatch: 'full',
      },
      {
        path: 'logisticians',
        component: LogisticiansPageComponent,
        canActivate: [authGuard],
        title: 'Logisticians',
      },
      {
        path: 'trips',
        component: TripsPageComponent,
        canActivate: [authGuard],
        title: 'Trips',
        children: [],
      },
      {
        path: 'trip',
        component: TripComponent,
        canActivate: [authGuard],
        title: 'Trip View',
      },
    ],
  },
  {
    path: 'trip-config',
    component: TripConfigPageComponent,
    canActivate: [authGuard],
    title: 'Trip Configuration',
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login',
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Register',
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [authGuard],
    title: 'Profile',
  },
];
