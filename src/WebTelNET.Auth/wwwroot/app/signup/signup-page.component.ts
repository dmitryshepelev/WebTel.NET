import { Component, ViewChild, OnInit } from "@angular/core";
import { ResponseModel } from "../shared/service";
import { AlertComponent, AlertType } from "../shared/controls/alert.component";
import { SignUpFormComponent } from "./signup-form.component";


@Component({
    moduleId: module.id,
    templateUrl: "signup-page.html"
})
export class SignUpPageComponent {

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    onSignupRequestSuccess(response: ResponseModel): void {
        this.alertComponent.message = response.message;
        this.alertComponent.type = AlertType.Success;
        this.alertComponent.show();
    }

    onSignupRequestError(error: ResponseModel): void {
        this.alertComponent.message = error.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }
}