import { Component, OnInit } from "@angular/core";
import { LoginModel } from "./login-model";

@Component({
    moduleId: module.id,
    templateUrl: "login-page.html"
})
export class LoginPageComponent implements OnInit {
    constructor() {}

    ngOnInit () {
        console.log(this);
    }

    onSubmit () {
        console.log(this);
    }
}