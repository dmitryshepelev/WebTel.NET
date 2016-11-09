import { Routes, RouterModule } from '@angular/router';

import { LoginPageComponent } from './login/login-page.component';
import { SignUpPageComponent } from "./signup/signup-page.component";


const appRoutes: Routes = [
    { path: "", redirectTo: "login", pathMatch: "full" },
    { path: "login", component: LoginPageComponent },
    { path: "signup", component: SignUpPageComponent },
    { path: "**", redirectTo: "" }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);