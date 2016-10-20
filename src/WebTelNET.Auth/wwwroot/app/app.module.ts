import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";

import { AppComponent }  from "./app.component";
import { LoginPageComponent } from "./login/login-page.component";
import { LoginFormComponent } from "./login/login-form.component";
import { RequestPageComponent } from "./request/request-page.component";
import { RequestFormComponent } from "./request/request-form.component";
import { routing, appRoutingProviders } from "./app.routing";
import { AccountService } from "./shared/account.service";
import { AlertComponent } from "./shared/controls/alert.component";


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
        AccountService
    ],
    declarations: [
        AppComponent,
        LoginPageComponent,
        LoginFormComponent,
        RequestPageComponent,
        RequestFormComponent,
        AlertComponent
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }