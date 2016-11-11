import { Component, Inject, EventEmitter, Output } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { AccountService } from "../shared/account.service";
import { SignUpModel } from "./signup-model";
import { ResponseModel } from "../shared/service";

import { ISubmitable, SubmitingComponent } from "../shared/libs/submiting.component";

@Component({
    moduleId: module.id,
    selector: "signup-form",
    templateUrl: "signup-form.html"
})
export class SignUpFormComponent extends SubmitingComponent implements ISubmitable {
    form: FormGroup;

    @Output()
    onSignupRequestSuccess = new EventEmitter<ResponseModel>();
    @Output()
    onSignupRequestError = new EventEmitter<ResponseModel>();
    @Output()
    onSubmitingStart = new EventEmitter<any>();
    @Output()
    onSubmitingEnd = new EventEmitter<any>();

    constructor(private accountService: AccountService, @Inject(FormBuilder) builder: FormBuilder) {
        super();
        this.form = builder.group({
            login: ["", Validators.required],
            email: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    submit() {
        const model = new SignUpModel(this.form.controls["login"].value, this.form.controls["email"].value, this.form.controls["password"].value);
        this.startSubmiting();
        this.accountService.signup(model)
            .then(response => this.onSignupRequestSuccess.emit(response))
            .catch(error => this.onSignupRequestError.emit(error))
            .then(() => this.endSubmiting());
    }

    startSubmiting(): void {
        super.startSubmiting();
        this.onSubmitingStart.emit();
    }
}