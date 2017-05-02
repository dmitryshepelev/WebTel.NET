import { Injectable, Output, EventEmitter } from '@angular/core';
import { Http, Response } from '@angular/http';

import { ServiceBase, ResponseModel } from "@commonclient/services";
import { UserServiceStatus, IServiceStatus } from "../models";


export interface IOfficeService {
    onServiceStatusChanged: EventEmitter<IServiceStatus>;

    getUserServices(): Promise<ResponseModel>;
    activateService(serviceType: string, data?: Object): Promise<ResponseModel>;
    getServiceData(serviceType: string): Promise<ResponseModel>;

    changeServiceStatus(serviceType: string, status: UserServiceStatus ): void;
}


@Injectable()
export class OfficeService extends ServiceBase implements IOfficeService {
    private _baseUrl: string = "/api/office";

    @Output() onServiceStatusChanged = new EventEmitter<IServiceStatus>();

    constructor(private _http: Http) {
        super(_http);
    }

    getUserServices(): Promise<ResponseModel> {
        return this.post(this._baseUrl + "/getservices/", {});
    }

    activateService(serviceType: string, data?: Object): Promise<ResponseModel> {
        return this.post(this._baseUrl + "/activateservice/", { ServiceTypeName: serviceType, ActivationData: data || {} });
    }

    getServiceData(serviceType: string): Promise<ResponseModel> {
        return this.post(this._baseUrl + "/getservicedata/", { ServiceTypeName: serviceType });
    }

    changeServiceStatus(serviceType: string, status: UserServiceStatus ): void {
        this.onServiceStatusChanged.emit({ serviceType: serviceType, status: status });
    }
}