import { Component, Inject, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ResponseModel } from "@commonclient/services";

import { IRequiredDataForm } from "./required-data-form.interface";
import { OfficeService, IOfficeService } from "../services/office.service";

import { UserServiceInfo, UserServiceStatus } from "../models"

@Component({
    moduleId: module.id,
    selector: "cloud-storage-required-data-form",
    templateUrl: "cloud-storage-required-data-form.html"
})
export class CloudStorageRequiredDataForm implements IRequiredDataForm {
    form: FormGroup;
    actionExecuting: boolean = false;

    @Output() onCloudStorageActivated: EventEmitter<boolean> = new EventEmitter<boolean>();

    constructor(
        @Inject(FormBuilder) private _builder: FormBuilder,
        @Inject(OfficeService) private _officeService: IOfficeService
    ) {
        this.form = _builder.group({
            Token: ["", Validators.required]
        });
    }

    getToken() {
        var url = "https://oauth.yandex.ru/authorize?response_type=token&client_id=6960bbe659954f85be659a57c4ef0dd8";
        var w = window.open(url, "_blank");
        w.focus();
    }

    activate() {
        this.actionExecuting = true;
        this._officeService.activateService("CloudStorage", { Token: this.form.controls["Token"].value })
            .then((response: ResponseModel) => {
                this.onCloudStorageActivated.emit(true);
            })
            .catch(error => {
                console.log(error);
            })
            .then(() => {
                this.actionExecuting = false;
            });
    }
}