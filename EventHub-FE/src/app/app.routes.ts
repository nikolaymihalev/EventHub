import { Routes } from '@angular/router';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { ErrorComponent } from './error/error.component';
import { AboutComponent } from './about/about.component';
import { AuthGuard } from './guards/auth.guard';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: MainComponent },
    { path: 'about', component: AboutComponent },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {
      path: 'event',
      children: [
        {
          path: ':eventId',
          loadComponent: () =>
              import('./events/event-details/event-details.component').then((c) => c.EventDetailsComponent)
        },
      ],
    },
    { path: 'add-event', loadComponent: () =>
        import('./events/add-event/add-event.component').then(
        (c) => c.AddEventComponent), canActivate: [AuthGuard],
    },
    {
      path: 'myevents',
      children: [
        { 
          path: '', 
          loadComponent: () =>
              import('./events/my-events/my-events.component').then(
              (c) => c.MyEventsComponent), canActivate: [AuthGuard],
        },
        {
          path: ':eventId',
          loadComponent: () =>
              import('./events/edit-event/edit-event.component').then(
              (c) => c.EditEventComponent), canActivate: [AuthGuard],
        },
      ],
    },
    { path: 'profile', loadComponent: () =>
      import('./user/profile/profile.component').then(
      (c) => c.ProfileComponent), canActivate: [AuthGuard],
    },
    { path: 'registrations', loadComponent: () =>
      import('./events/event-registrations/event-registrations.component').then(
      (c) => c.EventRegistrationsComponent), canActivate: [AuthGuard],
    },
    { path: '404', component: ErrorComponent },
    { path: '**', redirectTo: '/404' },
];
