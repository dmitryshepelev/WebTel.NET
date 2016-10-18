import { Routes, RouterModule } from '@angular/router';

import { LoginPageComponent } from './login/login-page.component';
import { SignupPageComponent } from "./signup/signup-page.component";


const appRoutes: Routes = [
    { path: '', component: LoginPageComponent },
    { path: 'signup', component: SignupPageComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);