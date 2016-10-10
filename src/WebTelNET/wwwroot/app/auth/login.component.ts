import { Component, OnInit } from "@angular/core"

@Component({
    moduleId: module.id,
    templateUrl: 'login.component.html'
})
export class LoginComponent implements OnInit {

    loginVm: any = {};

    constructor() {}

    ngOnInit () {
        console.log(this)
    }
}