import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { SidebarModule } from "ng-sidebar";

import { ServicesModule, StorageService } from "@commonclient/services";
import { AlertComponent } from "@commonclient/controls";
import { ButtonSpinnerDirective } from "@commonclient/directives";

import { OfficeAppComponent }  from './office-app.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { routing, appRoutingProviders } from './office-app.routing';
import { CharacterService } from './shared/character.service';

import { OfficeService } from "./shared/services/office.service";
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
        CharacterService,
        OfficeService
    ],
    declarations: [
        OfficeAppComponent,
        HomeComponent,
        AboutComponent,
        AlertComponent,
        ServicesPageComponent,

        ButtonSpinnerDirective
    ],
    bootstrap: [OfficeAppComponent]
})
export class AppModule { }