﻿import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { ServiceBase } from "@commonclient/services";
import { LoginModel } from "../login/login-model";
import { SignUpModel } from "../signup/signup-model";


@Injectable()
export class AccountService extends  ServiceBase {
    private baseUrl = "/api/account";

    constructor(http: Http) { super(http); }

    login(model: LoginModel): Promise<any> {
        return this.post(this.baseUrl + "/login/", model);
    };

    signup(model: SignUpModel): Promise<any> {
        return this.post(this.baseUrl + "/signup/", model);
    }
}