import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { LoginPageComponent } from './auth/login-page.component';

const appRoutes: Routes = [
    { path: '', component: LoginPageComponent },
    { path: 'home', component: HomeComponent },
    { path: 'about', component: AboutComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);