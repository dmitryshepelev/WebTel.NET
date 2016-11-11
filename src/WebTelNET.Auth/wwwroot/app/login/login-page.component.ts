import { Component, ViewChild, OnInit } from "@angular/core";
import { LoginModel } from "./login-model";
import { LoginFormComponent } from "./login-form.component";
import { AlertComponent, AlertType } from "../shared/controls/alert.component";
import { ResponseModel } from "../shared/service";
import { Router, ActivatedRoute, Params } from "@angular/router";


@Component({
    moduleId: module.id,
    templateUrl: "login-page.html"
})
export class LoginPageComponent implements OnInit {
    private redirectTo: string;

    constructor(private route: ActivatedRoute, private router: Router) { }

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    onLoginRequestSuccess(response: ResponseModel) {
        window.location.href = this.redirectTo || response.data.redirectUrl;
    }

    onLoginRequestError(error: ResponseModel) {
        this.alertComponent.message = error.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }

    onSubmitingStart(event: any) {

    }

    ngOnInit(): void {
        this.route.queryParams.forEach((params: Params) => {
            this.redirectTo = params["then"];
        });
    }
}