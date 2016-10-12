import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent }  from './app.component';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { LoginPageComponent } from './auth/login-page.component';
import { LoginForm } from './auth/login-form.component';
import { routing, appRoutingProviders } from './app.routing';
import { CharacterService } from './shared/character.service';
import { AccountService } from './shared/account.service'

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        routing
    ],
    providers: [
        appRoutingProviders,
        CharacterService,
        AccountService
    ],
    declarations: [
        AppComponent,
        HomeComponent,
        AboutComponent,
        LoginPageComponent,
        LoginForm
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }