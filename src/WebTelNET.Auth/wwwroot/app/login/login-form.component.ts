import { Component, Inject, EventEmitter, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../shared/account.service";
import { LoginModel } from "./login-model";
import { ResponseModel } from "../shared/service";

import { SubmitingComponent } from "../shared/interfaces/isubmitable";

@Component({
    moduleId: module.id,
    selector: "login-form",
    templateUrl: "login-form.html"
})
export class LoginFormComponent extends SubmitingComponent {
    form: FormGroup;

    @Output()
    onLoginRequestSuccess = new EventEmitter<ResponseModel>();
    @Output()
    onLoginRequestError = new EventEmitter<ResponseModel>();

    @Output()
    onLoginFormSubmitingStart = new EventEmitter<any>();

    constructor(private accountService: AccountService, @Inject(FormBuilder) builder: FormBuilder) {
        super();
        this.form = builder.group({
            login: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    submit() {
        const model = new LoginModel(this.form.controls["login"].value, this.form.controls["password"].value);
        this.submiting = true;
        this.onLoginFormSubmitingStart.emit();
        this.accountService.login(model)
            .then(response => this.onLoginRequestSuccess.emit(response))
            .catch(error => this.onLoginRequestError.emit(error))
            .then(() => this.submiting = false);
    }
}