import { Routes, RouterModule } from '@angular/router';

import { StatisticPageComponent } from './statistic/statistic-page.component';
import { CallCostPageComponent } from "./call-cost/call-cost-page.component";
import { CallbackPageComponent } from "./callback/callback-page.component";


const appRoutes: Routes = [
    { path: '', redirectTo: "statistic", pathMatch: "full" },
    { path: 'statistic', component: StatisticPageComponent },
    { path: 'cost', component: CallCostPageComponent },
    { path: 'callback', component: CallbackPageComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);