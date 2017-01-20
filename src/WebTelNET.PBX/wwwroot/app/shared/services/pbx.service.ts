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

    getBalance(): Promise<any> {
        return this.post(this.baseUrl + "/balance/", {});
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
        return this.post(this.baseUrl + "/statistics/", { start: start, end: end });
    }

    getCallRecordLink(pbxcallid: string): Promise<any> {
        return this.post(this.baseUrl + "/callrecord/", { PbxCallId: pbxcallid });
    }

    getWidgetId(): Promise<any> {
        return this.post(this.baseUrl + "/widgetid/", {});
    }

    afterExecuting(response: any): any {
        console.info("Request completed: -> ", response);
        return response;
    }

}