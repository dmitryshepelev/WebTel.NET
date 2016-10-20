import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { ServiceBase } from "./service";
import { LoginModel } from "../login/login-model";
import { RequestModel } from "../request/request-model";


@Injectable()
export class AccountService extends  ServiceBase {
    private baseUrl = "/api/account";

    constructor(http: Http) { super(http); }

    login(model: LoginModel): Promise<any> {
        return this.post(this.baseUrl + "/login/", model);
    };

    request(model: RequestModel): Promise<any> {
        return this.post(this.baseUrl + "/request/", model);
    }


}