import { Component, ViewChild } from "@angular/core";
import { ResponseModel } from "../shared/service";
import { AlertComponent, AlertType } from "../shared/controls/alert.component";
import { SignupFormComponent } from "./signup-form-component";


@Component({
    moduleId: module.id,
    templateUrl: "signup-page.html"
})
export class SignupPageComponent {

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    onSignupRequestSuccess(response: ResponseModel): void {
        this.alertComponent.message = response.message;
        this.alertComponent.type = response.type === 0 ? AlertType.Success : AlertType.Error;
        this.alertComponent.show();
    }

    onSignupRequestError(error: ResponseModel): void {
        this.alertComponent.message = error.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }
}