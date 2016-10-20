import { Http, RequestOptionsArgs } from "@angular/http";
import { Observable } from "rxjs";

import "rxjs/add/operator/toPromise"


export enum ResponseType {
    Success,
    Error,
    Unavailable
}

export class ResponseModel {
    message: string;
    data: any;
    type: ResponseType;
}

export abstract class ServiceBase {

    constructor(protected http: Http) { }

    private handleError(error: any): Promise<any> {
        var errorObj = error.json();

        var responseModel = new ResponseModel();
        if (errorObj instanceof ProgressEvent) {
            const serverUnavailableError: string = "Сервер временно недоступен. Повторите попытку позже.";
            responseModel.message = serverUnavailableError;
            responseModel.data = errorObj;
            responseModel.type = ResponseType.Unavailable;

            return Promise.reject(responseModel);
        }

        responseModel.message = errorObj.message;
        responseModel.data = errorObj.data;
        responseModel.type = ResponseType.Error;

        return Promise.reject(responseModel);
    }

    private handleSuccess(response: any): Promise<ResponseModel> {
        var responseObj = response.json();

        var responseModel = new ResponseModel();
        responseModel.message = responseObj.message;
        responseModel.data = responseObj.data;
        responseModel.type = ResponseType.Success;

        return Promise.resolve(responseModel);
    }

    post(url: string, body: any, options?: RequestOptionsArgs): Promise<ResponseModel> {
        return this.http.post(url, body, options)
            .toPromise()
            .then(this.handleSuccess)
            .catch(this.handleError);
    }
}