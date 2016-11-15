import { Component, ViewChild, OnInit } from "@angular/core";
import { LoginModel } from "./login-model";
import { LoginFormComponent } from "./login-form.component";
import { AlertComponent, AlertType, AlertModel } from "../shared/controls/alert.component";
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

    private showAlert(alert: AlertModel) {
        this.alertComponent.message = alert.message;
        this.alertComponent.type = alert.type;
        this.alertComponent.show();
    }

    onLoginRequestSuccess(response: ResponseModel) {
        window.location.href = this.redirectTo || response.data.redirectUrl;
    }

    onLoginRequestError(error: ResponseModel) {
        var alert = new AlertModel();
        alert.message = error.message;
        alert.type = AlertType.Error;
        this.showAlert(alert);
    }

    onSubmitingStart(event: any) {

    }

    ngOnInit(): void {
        this.route.queryParams.forEach((params: Params) => {
            this.redirectTo = params["then"];
        });
    }
}