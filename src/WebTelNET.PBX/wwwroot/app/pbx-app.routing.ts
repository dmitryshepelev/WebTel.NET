import { Routes, RouterModule } from '@angular/router';

import { StatisticPageComponent } from './statistic/statistic-page.component';
import { CallCostPageComponent } from "./call-cost/call-cost-page.component";


const appRoutes: Routes = [
    { path: '', redirectTo: "statistic", pathMatch: "full" },
    { path: 'statistic', component: StatisticPageComponent },
    { path: 'cost', component: CallCostPageComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);