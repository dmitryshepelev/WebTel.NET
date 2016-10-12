import { Injectable } from "@angular/core";
import { Http, Headers } from "@angular/http";
import { LoginModel } from "../auth/login-model.ts";

import "rxjs/add/operator/toPromise";

@Injectable()
export class AccountService {
    constructor(private http: Http) { }

    login(model: LoginModel): Promise<ResponseModel> {
        return this.http.post("api/account/", model)
            .toPromise()
            .then(response => response.json() as ResponseModel)
            .catch(response => console.log(response.json() as ResponseModel));
    }
}


export class ResponseModel {
    constructor() { }

    message: string;
    data: any;
}