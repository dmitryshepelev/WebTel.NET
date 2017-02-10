import { Component, Inject, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { PBXService } from "../../app/shared/services/pbx.service";

import { ISubmitable, SubmitingComponent } from "@commonclient/components";

@Component({
    moduleId: module.id,
    templateUrl: "setup-page.html"
})
export class SetupPageComponent extends SubmitingComponent implements ISubmitable {
    @Output()
    onSubmitingStart = new EventEmitter<any>();
    @Output()
    onSubmitingEnd = new EventEmitter<any>();

    form: FormGroup;

    constructor(
        private _pbxService: PBXService,
        @Inject(FormBuilder) private _builder: FormBuilder
    ) {
        super();
        this.form = this._builder.group({
            userKey: ["", Validators.required],
            secretKey: ["", Validators.required]
        });
    }

    onSubmit() {
        this.startSubmiting();
        this._pbxService.createAccount(this.form.controls["userKey"].value, this.form.controls["secretKey"].value)
            .then(response => { location.reload(true); })
            .catch(error => console.log(error))
            .then(() => this.endSubmiting());
    }

    startSubmiting(): void {
        super.startSubmiting();
        this.onSubmitingStart.emit();
    }
}