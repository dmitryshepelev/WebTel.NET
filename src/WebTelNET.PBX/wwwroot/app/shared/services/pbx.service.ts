import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ServiceBase } from "@commonclient/services";

import { CallbackModel } from "../models";



@Injectable()
export class PBXService extends ServiceBase {
    private baseUrl = "/api/pbx";

    constructor(http: Http) {
        super(http);
    }

    getPriceInfo(number: string): Promise<any> {
        return this.post(this.baseUrl + "/priceinfo/", { number: number });
    }

    callback() { }

    getPBXStatistics(): Promise<any> {
        return this.post(this.baseUrl + "/pbxstatistics/", {});
    }

    getOverallStatistics(): Promise<any> {
        return this.post(this.baseUrl + "/overallstatistics/", {});
    }
}