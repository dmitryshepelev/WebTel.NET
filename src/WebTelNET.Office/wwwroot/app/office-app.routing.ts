import { Routes, RouterModule } from '@angular/router';

import { ServicesPageComponent } from './services/services-page.component';
import { AboutComponent } from './about/about.component';

const appRoutes: Routes = [
    { path: '', component: ServicesPageComponent },
    { path: 'about', component: AboutComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);