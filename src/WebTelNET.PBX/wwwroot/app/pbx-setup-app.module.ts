import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, Http } from '@angular/http';
import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";

import { ServicesModule, StorageService } from "@commonclient/services";
import { AlertComponent } from "@commonclient/controls";
import { ButtonSpinnerDirective } from "@commonclient/directives";

import { PBXSetupAppComponent }  from './pbx-setup-app.component';
import { routing, appRoutingProviders } from './pbx-setup-app.routing';
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
        TranslateModule.forRoot()
    ],
    providers: [
        appRoutingProviders,
        PBXService,
        StorageService
    ],
    declarations: [
        PBXSetupAppComponent,
        AlertComponent,

        ButtonSpinnerDirective
    ],
    bootstrap: [PBXSetupAppComponent]
})
export class AppModule {

}