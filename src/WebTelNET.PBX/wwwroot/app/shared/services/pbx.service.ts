import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ServiceBase } from "@commonclient/services";

import { CallbackModel, StatisticsParamsModel } from "../models";

import * as moment from "moment";

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

    getPBXStatistics(start: Date, end: Date): Promise<any> {
        return this.post(this.baseUrl + "/pbxstatistics/", { start: start, end: end });
    }

    getOverallStatistics(start: Date, end: Date): Promise<any> {
        return this.post(this.baseUrl + "/overallstatistics/", { start: start, end: end });
    }

    getStatistics(start: Date, end: Date): Promise<any> {
        return this.post(this.baseUrl + "/statistics/", {});
    }
}