import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";

import { AuthAppComponent }  from "./auth-app.component";
import { LoginPageComponent } from "./login/login-page.component";
import { LoginFormComponent } from "./login/login-form.component";
import { SignUpPageComponent } from "./signup/signup-page.component";
import { SignUpFormComponent } from "./signup/signup-form.component";
import { routing, appRoutingProviders } from "./auth-app.routing";
import { AccountService } from "./shared/account.service";
import { StorageService } from "@commonclient/services";
import { AlertComponent } from "@commonclient/controls";


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
        AccountService,
        StorageService
    ],
    declarations: [
        AuthAppComponent,
        LoginPageComponent,
        LoginFormComponent,
        SignUpPageComponent,
        SignUpFormComponent,
        AlertComponent
    ],
    bootstrap: [AuthAppComponent]
})
export class AuthAppModule { }