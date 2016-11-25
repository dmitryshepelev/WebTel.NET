import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ServiceBase } from "@commonclient/services";


@Injectable()
export class PBXService extends ServiceBase {
    private baseUrl = "/api/pbx";

    constructor(http: Http) {
        super(http);
    }

    getPriceInfo(number: string): Promise<any> {
        return this.post(this.baseUrl + '/priceinfo/', { number: number });
    }
}