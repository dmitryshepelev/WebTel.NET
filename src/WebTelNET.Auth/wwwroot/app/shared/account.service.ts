import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { ServiceBase } from "./service";
import { LoginModel } from "../login/login-model";
import { SignupModel } from "../signup/signup-model";


@Injectable()
export class AccountService extends  ServiceBase {
    private baseUrl = "/api/account";

    constructor(http: Http) { super(http); }

    login(model: LoginModel): Promise<any> {
        return this.post(this.baseUrl + "/login/", model);
    };

    signup(model: SignupModel): Promise<any> {
        return this.post(this.baseUrl + "/signup/", model);
    }


}


