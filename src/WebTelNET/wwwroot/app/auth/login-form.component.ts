import { Component, OnInit, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { AccountService as AccountServcie } from "../shared/account.service";
import { LoginModel } from "./login-model"

@Component({
    moduleId: module.id,
    selector: "login-form",
    templateUrl: "login-form.html"
})
export class LoginForm implements OnInit {
    form: FormGroup;

    constructor(private accountService: AccountServcie, @Inject(FormBuilder) builder: FormBuilder) {

        this.form = builder.group({
            login: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    submit() {
        var model = new LoginModel(this.form.controls["login"].value, this.form.controls["password"].value);
        this.accountService.login(model)
            .then(response => { console.log(response); })
            .catch(error => { console.log(error);});
    }


    ngOnInit(): void {
        
    }
}