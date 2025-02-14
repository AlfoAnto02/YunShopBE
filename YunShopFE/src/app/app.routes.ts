import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { ProfileComponent } from './components/profile/profile.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'SignUp', component: SignUpComponent },
    { path: 'SignIn', component: SignInComponent },
    { path: 'Profile', component: ProfileComponent, canActivate: [authGuard] }
];
