import { Component, Inject, Output, Input, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ResponseModel } from "@commonclient/services";
import { SubmitingComponent, ISubmitable } from "@commonclient/components";

import { IRequiredDataForm } from "./required-data-form.interface";
import { OfficeService, IOfficeService } from "../services/office.service";
import { UserServiceInfo, UserServiceStatus, DynamicComponentMode, IDynamicComponent, IDynamicComponentSettings } from "../models"

@Component({
    moduleId: module.id,
    selector: 'pbx-required-data-form',
    templateUrl: 'pbx-required-data-form.html'
})
export class PBXRequiredDataForm extends SubmitingComponent implements IRequiredDataForm, IDynamicComponent {
    private _serviceTypeName: string = "PBX";

    form: FormGroup;

    constructor(
        @Inject(FormBuilder) private _builder: FormBuilder,
        @Inject(OfficeService) private _officeService: IOfficeService
    ) { 
        super();

        this.form = _builder.group({
            UserKey: ["", Validators.required],
            SecretKey: ["", Validators.required]
        });
    }

    ngOnInit() {}

    activate(): void {
        this.startSubmiting();
        this._officeService.activateService(this._serviceTypeName, { UserKey: this.form.controls["UserKey"].value, SecretKey: this.form.controls["SecretKey"].value })
            .then((response: ResponseModel) => {
                this._officeService.changeServiceStatus(this._serviceTypeName, UserServiceStatus.Activated);
            })
            .catch(error => {
                console.log(error);
            })
            .then(() => { this.endSubmiting(); })
    }

    init(settings: IDynamicComponentSettings) {
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