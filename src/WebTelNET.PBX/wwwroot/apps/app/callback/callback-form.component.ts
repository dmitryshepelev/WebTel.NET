import { Component, Inject, EventEmitter, Output, trigger, transition, style, animate, state } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from "@angular/forms";
import { SubmitingComponent, ISubmitable } from "@commonclient/components";

import { ResponseModel } from "@commonclient/services";
import { CallbackModel } from "../shared/models";
import { PBXService } from "../shared/services/pbx.service";
import { PriceInfoModel } from "../shared/components/price-info.component";

import { inOut } from "../shared/animations";


@Component({
    moduleId: module.id,
    animations: [
        inOut
    ],
    selector: "callback-form",
    templateUrl: "callback-form.html"
})
export class CallbackFormComponent extends SubmitingComponent {
    private _fromControlName = "from";
    private _toControlName = "to";

    form: FormGroup;
    mask = ["+", /[1-9]/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/];
    placeholderChar = "\u2000";
    fromPriceInfo: PriceInfoModel;
    toPriceInfo: PriceInfoModel;
    fromLoading = false;
    toLoading = false;

    @Output()
    onFormSubmitSuccess = new EventEmitter<ResponseModel>();
    @Output()
    onFormSubmitFailure = new EventEmitter<ResponseModel>();

    constructor(private pbxService: PBXService, @Inject(FormBuilder) builder: FormBuilder) {
        super();
        this.form = builder.group({
            from: ["", [Validators.required]],
            to: ["", Validators.required]
        });
    }

    onFromChanged() {
        this.fromPriceInfo = null;
        const value = this.form.controls[this._fromControlName].value;

        if (!value) return;

        this.fromLoading = true;
        this.pbxService.getPriceInfo(value)
            .then(response => this.fromPriceInfo = <PriceInfoModel>response.data.Info)
            .catch(error => console.log(error))
            .then(() => this.fromLoading = false);
    }

    onToChanged() {
        this.toPriceInfo = null;
        const value = this.form.controls[this._toControlName].value;

        if (!value) return;

        this.toLoading = true;
        this.pbxService.getPriceInfo(value)
            .then(response => this.toPriceInfo = <PriceInfoModel>response.data.Info)
            .catch(error => console.log(error))
            .then(() => this.toLoading = false);
    }

    replaceNumbers() {
        var temp = this.form.controls[this._fromControlName].value;
        this.form.controls[this._fromControlName].setValue(this.form.controls[this._toControlName].value);
        this.form.controls[this._toControlName].setValue(temp);
        this.onFromChanged();
        this.onToChanged();
    }

    submit() {
        const model = new CallbackModel(this.form.controls[this._fromControlName].value, this.form.controls[this._toControlName].value);
        this.startSubmiting();
        this.pbxService.callback(model.from, model.to)
            .then(response => this.onFormSubmitSuccess.emit(response))
            .catch(error => this.onFormSubmitFailure.emit(error))
            .then(() => { this.endSubmiting(); });
    }
}