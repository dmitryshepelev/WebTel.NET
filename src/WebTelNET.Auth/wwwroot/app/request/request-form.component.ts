import { Component, Inject, EventEmitter, Output } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { AccountService } from "../shared/account.service";
import { RequestModel } from "./request-model";
import { ResponseModel } from "../shared/service";


@Component({
    moduleId: module.id,
    selector: "request-form",
    templateUrl: "request-form.html"
})
export class RequestFormComponent {
    form: FormGroup;
    submittedSucceed = false;

    @Output()
    onSignupRequestSuccess = new EventEmitter<ResponseModel>();
    @Output()
    onSignupRequestError = new EventEmitter<ResponseModel>();

    constructor(private accountService: AccountService, @Inject(FormBuilder) builder: FormBuilder) {

        this.form = builder.group({
            login: ["", Validators.required],
            email: ["", Validators.required]
        });
    }

    submit() {
        const model = new RequestModel(this.form.controls["login"].value, this.form.controls["email"].value);
        console.log(this.form);
        this.accountService.request(model)
            .then(response => this.onSignupRequestSuccess.emit(response))
            .catch(error => this.onSignupRequestError.emit(error));
    }
}