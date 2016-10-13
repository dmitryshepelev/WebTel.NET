import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { ServiceBase } from "./service";
import { LoginModel } from "../auth/login-model";


@Injectable()
export class AccountService extends  ServiceBase {
    constructor(http: Http) { super(http); }

    login(model: LoginModel): Promise<any> {
        return this.post("api/account/", model);
    }
}


