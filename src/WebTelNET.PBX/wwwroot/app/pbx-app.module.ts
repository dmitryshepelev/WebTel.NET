import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { ServicesModule, StorageService } from "@commonclient/services";

import { PBXAppComponent }  from './pbx-app.component';
import { StatisticPageComponent } from './statistic/statistic-page.component';
import { CallCostPageComponent } from './call-cost/call-cost-page.component';
import { CallbackPageComponent } from "./callback/callback-page.component";
import { CallbackFormComponent } from "./callback/callback-form.component";
import { routing, appRoutingProviders } from './pbx-app.routing';
import { CharacterService } from './shared/character.service';


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        routing,
        ServicesModule
    ],
    providers: [
        appRoutingProviders,
        CharacterService,
        StorageService
    ],
    declarations: [
        PBXAppComponent,
        StatisticPageComponent,
        CallCostPageComponent,
        CallbackFormComponent,
        CallbackPageComponent,
    ],
    bootstrap: [PBXAppComponent]
})
export class AppModule { }