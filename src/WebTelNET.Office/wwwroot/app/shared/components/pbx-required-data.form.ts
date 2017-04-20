import { Component, Inject, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ResponseModel } from "@commonclient/services";
import { SubmitingComponent, ISubmitable } from "@commonclient/components";

import { IRequiredDataForm } from "./required-data-form.interface";
import { OfficeService, IOfficeService } from "../services/office.service";

import { UserServiceInfo, UserServiceStatus } from "../models"

@Component({
    moduleId: module.id,
    selector: 'pbx-required-data-form',
    templateUrl: 'pbx-required-data-form.html'
})
export class PBXRequiredDataForm extends SubmitingComponent implements IRequiredDataForm {
    form: FormGroup;

    @Output() onPBXActivated: EventEmitter<boolean> = new EventEmitter<boolean>();

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

    ngOnInit() { 
        
    }

    activate(): void {
        this.startSubmiting();
        this._officeService.activateService("PBX", { UserKey: this.form.controls["UserKey"].value, SecretKey: this.form.controls["SecretKey"].value })
            .then((response: ResponseModel) => {
                this.onPBXActivated.emit(true);
            })
            .catch(error => {
                console.log(error);
            })
            .then(() => { this.endSubmiting(); })
    }
}