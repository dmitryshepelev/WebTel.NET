import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { TextMaskModule } from "angular2-text-mask";
import { DatepickerModule } from "ng2-bootstrap/ng2-bootstrap";
import { SidebarModule } from "ng-sidebar";
import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";

import { ServicesModule, StorageService } from "@commonclient/services";
import { AlertComponent, PlayerComponent } from "@commonclient/controls";
import { ButtonSpinnerDirective } from "@commonclient/directives";

import { PBXAppComponent }  from './pbx-app.component';
import { StatisticsPageComponent } from './statistics/statistics-page.component';
import { StatisticsFormComponent } from './statistics/statistics-form.component';
import { CallCardComponent } from "./statistics/call-card.component";
import { CallCostPageComponent } from './call-cost/call-cost-page.component';
import { CallbackPageComponent } from "./callback/callback-page.component";
import { CallbackFormComponent } from "./callback/callback-form.component";
import { GetScriptPageComponent } from "./getscript/get-script-page.component";
import { GetScriptFormComponent } from "./getscript/get-script-form.component";
import { routing, appRoutingProviders } from './pbx-app.routing';
import { PBXService } from './shared/services/pbx.service';
import { PriceInfoComponent } from "./shared/components/price-info.component";
import { BalanceCardComponent } from "./shared/components/balance-card.component";
import { ButtonSpinnerDirective } from "./shared/directives/button-spinner.directive";

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
        SidebarModule,
        TranslateModule.forRoot()
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
        PlayerComponent,
        PriceInfoComponent,
        BalanceCardComponent,
        GetScriptPageComponent,
        GetScriptFormComponent,

        ButtonSpinnerDirective
    ],
    bootstrap: [PBXAppComponent]
})
export class AppModule { }