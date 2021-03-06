﻿import { Component, Inject, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ResponseModel } from "@commonclient/services";
import { SubmitingComponent, ISubmitable } from "@commonclient/components";

import { IRequiredDataForm } from "./required-data-form.interface";
import { OfficeService, IOfficeService } from "../services/office.service";

import { UserServiceInfo, UserServiceStatus, DynamicComponentMode, IDynamicComponent, IDynamicComponentSettings } from "../models";

@Component({
    moduleId: module.id,
    selector: "cloud-storage-required-data-form",
    templateUrl: "cloud-storage-required-data-form.html"
})
export class CloudStorageRequiredDataForm extends SubmitingComponent implements IRequiredDataForm, IDynamicComponent {
    private _serviceTypeName: string = "CloudStorage";

    form: FormGroup;
    mode: DynamicComponentMode;

    constructor(
        @Inject(FormBuilder) private _builder: FormBuilder,
        @Inject(OfficeService) private _officeService: IOfficeService
    ) {
        super();

        this.form = _builder.group({
            Token: ["", Validators.required]
        });
    }

    get EditMode(): boolean {
        return this.mode == DynamicComponentMode.EDIT;
    }

    getToken() {
        var url = "https://oauth.yandex.ru/authorize?response_type=token&client_id=6960bbe659954f85be659a57c4ef0dd8";
        var w = window.open(url, "_blank");
        w.focus();
    }

    submit(): void {
        this.EditMode ? this.edit() : this.activate();
    }

    activate() {
        this.startSubmiting();
        this._officeService.activateService(this._serviceTypeName, { Token: this.form.controls["Token"].value })
            .then((response: ResponseModel) => {
                this._officeService.changeServiceStatus(this._serviceTypeName, UserServiceStatus.Activated);
            })
            .catch(error => {
                console.log(error);
            })
            .then(() => {
                this.endSubmiting();
            });
    }

    edit(): void {
        this.startSubmiting();
        this._officeService.editServiceData(this._serviceTypeName, { Token: this.form.controls["Token"].value })
            .then((response: ResponseModel) => {

            })
            .catch(error => {
                console.log(error);
            })
            .then(() => {
                this.endSubmiting();
            });
    }

    init(settings: IDynamicComponentSettings) {
        this.mode = settings.mode;

        if (settings.mode == DynamicComponentMode.EDIT) {
            this._officeService.getServiceData(this._serviceTypeName)
                .then((response: ResponseModel) => {
                    this.form.setValue(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
    }
}