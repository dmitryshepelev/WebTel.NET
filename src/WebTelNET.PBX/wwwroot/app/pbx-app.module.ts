import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { PBXAppComponent }  from './pbx-app.component';
import { StatisticPageComponent } from './statistic/statistic-page.component';
import { CallCostPageComponent } from './call-cost/call-cost-page.component';
import { routing, appRoutingProviders } from './pbx-app.routing';
import { CharacterService } from './shared/character.service';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        routing
    ],
    providers: [
        appRoutingProviders,
        CharacterService
    ],
    declarations: [
        PBXAppComponent,
        StatisticPageComponent,
        CallCostPageComponent
    ],
    bootstrap: [PBXAppComponent]
})
export class AppModule { }