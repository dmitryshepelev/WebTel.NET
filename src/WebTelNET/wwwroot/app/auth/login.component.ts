import { Component, OnInit } from "@angular/core";
import { LoginModel } from "./login-model";

@Component({
    moduleId: module.id,
    templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {

    loginVm: LoginModel = new LoginModel();

    constructor() {}

    ngOnInit () {
        console.log(this);
    }

    onSubmit () {
        console.log(this);
    }
}