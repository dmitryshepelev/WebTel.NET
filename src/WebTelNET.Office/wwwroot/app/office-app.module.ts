import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { SidebarModule } from "ng-sidebar";

import { ServicesModule, StorageService } from "@commonclient/services";
import { AlertComponent } from "@commonclient/controls";
import { ButtonSpinnerDirective } from "@commonclient/directives";

import { OfficeAppComponent }  from './office-app.component';
import { AboutComponent } from './about/about.component';
import { routing, appRoutingProviders } from './office-app.routing';

import { OfficeService } from "./shared/services/office.service";
import { UserServiceControlComponent } from "./shared/components/user-service-control.component";
import { ServicesPageComponent } from "./services/services-page.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        routing,
        SidebarModule,
        ServicesModule
    ],
    providers: [
        appRoutingProviders,
        StorageService,
        OfficeService
    ],
    declarations: [
        OfficeAppComponent,
        AboutComponent,
        AlertComponent,

        ServicesPageComponent,

        UserServiceControlComponent,

        ButtonSpinnerDirective
    ],
    bootstrap: [OfficeAppComponent]
})
export class AppModule { }