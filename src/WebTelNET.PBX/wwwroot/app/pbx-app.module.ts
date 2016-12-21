import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { TextMaskModule } from "angular2-text-mask";
import { DatepickerModule } from "ng2-bootstrap/ng2-bootstrap";
import { SidebarModule } from "ng2-sidebar";

import { ServicesModule, StorageService } from "@commonclient/services";
import { AlertComponent } from "@commonclient/controls";

import { PBXAppComponent }  from './pbx-app.component';
import { StatisticsPageComponent } from './statistics/statistics-page.component';
import { StatisticsFormComponent } from './statistics/statistics-form.component';
import { CallCardComponent } from "./statistics/call-card.component";
import { CallCostPageComponent } from './call-cost/call-cost-page.component';
import { CallbackPageComponent } from "./callback/callback-page.component";
import { CallbackFormComponent } from "./callback/callback-form.component";
import { routing, appRoutingProviders } from './pbx-app.routing';
import { PBXService } from './shared/services/pbx.service';
import { PriceInfoComponent } from "./shared/components/price-info.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        routing,
        ServicesModule,
        TextMaskModule,
        DatepickerModule,
        SidebarModule
    ],
    providers: [
        appRoutingProviders,
        PBXService,
        StorageService
    ],
    declarations: [
        PBXAppComponent,
        StatisticsPageComponent,
        StatisticsFormComponent,
        CallCardComponent,
        CallCostPageComponent,
        CallbackFormComponent,
        CallbackPageComponent,
        AlertComponent,
        PriceInfoComponent
    ],
    bootstrap: [PBXAppComponent]
})
export class AppModule { }