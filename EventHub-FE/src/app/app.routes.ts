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
    { path: 'add-theme', loadComponent: () =>
        import('./events/add-event/add-event.component').then(
        (c) => c.AddEventComponent
        ), canActivate: [AuthGuard],
    },
    
    { path: '404', component: ErrorComponent },
    { path: '**', redirectTo: '/404' },
];
