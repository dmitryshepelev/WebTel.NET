import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import { ServiceBase, ResponseModel } from "@commonclient/services";



export interface IOfficeService {
    getUserServices(): Promise<ResponseModel>;
}


@Injectable()
export class OfficeService extends ServiceBase implements IOfficeService {
    private _baseUrl: string = "/api/office";

    constructor(private _http: Http) {
        super(_http);
    }

    getUserServices(): Promise<ResponseModel> {
        return this.post(this._baseUrl + "/getservices/", {});
    }
}