import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { PBXAppComponent }  from './pbx-app.component';
import { HomeComponent } from './home/home.component';
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
        HomeComponent
    ],
    bootstrap: [PBXAppComponent]
})
export class AppModule { }