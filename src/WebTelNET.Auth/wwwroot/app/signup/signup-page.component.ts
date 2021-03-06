import { Component, ViewChild, OnInit } from "@angular/core";
import { ResponseModel } from "@commonclient/services";
import { AlertComponent, AlertType, AlertModel } from "@commonclient/controls";
import { StorageService } from "@commonclient/services";
import { SignUpFormComponent } from "./signup-form.component";
import { Router, ActivatedRoute, Params } from "@angular/router";


@Component({
    moduleId: module.id,
    templateUrl: "signup-page.html"
})
export class SignUpPageComponent {
    constructor(private storageService: StorageService, private route: ActivatedRoute, private router: Router) {}

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    onSignupRequestSuccess(response: ResponseModel): void {
        var alert = new AlertModel();
        alert.message = response.message;
        alert.type = AlertType.Success;

        this.storageService.setItem("alert", alert);
        this.router.navigateByUrl("/login");
    }

    onSignupRequestError(error: ResponseModel): void {
        this.alertComponent.message = error.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }
}