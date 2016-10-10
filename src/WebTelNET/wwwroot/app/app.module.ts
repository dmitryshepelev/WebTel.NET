import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent }  from './app.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { LoginComponent } from './auth/login.component';
import { routing, appRoutingProviders } from './app.routing';
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
        AppComponent,
        HomeComponent,
        AboutComponent,
        LoginComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }