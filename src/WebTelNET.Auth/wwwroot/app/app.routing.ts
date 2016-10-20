import { Routes, RouterModule } from '@angular/router';

import { LoginPageComponent } from './login/login-page.component';
import { RequestPageComponent } from "./request/request-page.component";


const appRoutes: Routes = [
    { path: '', component: LoginPageComponent },
    { path: 'request', component: RequestPageComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);