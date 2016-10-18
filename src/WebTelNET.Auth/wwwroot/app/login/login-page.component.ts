import { Component, ViewChild } from "@angular/core";
import { LoginModel } from "./login-model";
import { LoginFormComponent } from "./login-form.component";
import { AlertComponent, AlertType } from "../shared/controls/alert.component";
import { ResponseModel } from "../shared/service";


@Component({
    moduleId: module.id,
    templateUrl: "login-page.html"
})
export class LoginPageComponent {

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    onLoginRequestCompleted(response: ResponseModel) {
        this.alertComponent.message = response.message;
        this.alertComponent.type = response.type === 0 ? AlertType.Success : AlertType.Error;
        this.alertComponent.show();
    }
}