import { Routes, RouterModule } from '@angular/router';
import { SetupPageComponent } from "./setup/setup-page.component";


const appRoutes: Routes = [
    { path: '', component: SetupPageComponent }
];

export const appRoutingProviders: any[] = [

];

export const routing = RouterModule.forRoot(appRoutes);