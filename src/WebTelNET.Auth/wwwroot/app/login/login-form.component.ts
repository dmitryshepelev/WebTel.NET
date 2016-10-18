import { Component, Inject, EventEmitter, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../shared/account.service";
import { LoginModel } from "./login-model";
import { ResponseModel } from "../shared/service";


@Component({
    moduleId: module.id,
    selector: "login-form",
    templateUrl: "login-form.html"
})
export class LoginFormComponent {
    form: FormGroup;

    @Output()
    onLoginRequestCompleted = new EventEmitter<ResponseModel>();

    constructor(private accountService: AccountService, @Inject(FormBuilder) builder: FormBuilder) {

        this.form = builder.group({
            login: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    submit() {
        const model = new LoginModel(this.form.controls["login"].value, this.form.controls["password"].value);
        this.accountService.login(model)
            .then(response => this.onLoginRequestCompleted.emit(response))
            .catch(error => this.onLoginRequestCompleted.emit(error));
    }
}