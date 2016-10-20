import { Component, ViewChild } from "@angular/core";
import { ResponseModel } from "../shared/service";
import { AlertComponent, AlertType } from "../shared/controls/alert.component";
import { RequestFormComponent } from "./request-form.component";


@Component({
    moduleId: module.id,
    templateUrl: "request-page.html"
})
export class RequestPageComponent {

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