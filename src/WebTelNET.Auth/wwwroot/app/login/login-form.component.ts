import { Component, Inject, EventEmitter, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountService } from "../shared/account.service";
import { LoginModel } from "./login-model";
import { ResponseModel } from "../shared/service";

import { ISubmitable, SubmitingComponent } from "../shared/libs/submiting.component";

@Component({
    moduleId: module.id,
    selector: "login-form",
    templateUrl: "login-form.html"
})
export class LoginFormComponent extends SubmitingComponent implements ISubmitable {
    form: FormGroup;

    @Output()
    onLoginRequestSuccess = new EventEmitter<ResponseModel>();
    @Output()
    onLoginRequestError = new EventEmitter<ResponseModel>();
    @Output()
    onSubmitingStart = new EventEmitter<any>();
    @Output()
    onSubmitingEnd = new EventEmitter<any>();

    constructor(private accountService: AccountService, @Inject(FormBuilder) builder: FormBuilder) {
        super();
        this.form = builder.group({
            login: ["", Validators.required],
            password: ["", Validators.required]
        });
    }
    
    submit() {
        const model = new LoginModel(this.form.controls["login"].value, this.form.controls["password"].value);
        this.startSubmiting();
        this.accountService.login(model)
            .then(response => this.onLoginRequestSuccess.emit(response))
            .catch(error => this.onLoginRequestError.emit(error))
            .then(() => this.endSubmiting());
    }

    startSubmiting(): void {
        super.startSubmiting();
        this.onSubmitingStart.emit();
    }
}