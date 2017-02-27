import { Component, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";


@Component({
    moduleId: module.id,
    selector: "cloud-storage-required-data-form",
    templateUrl: "cloud-storage-required-data-form.html"
})
export class CloudStorageRequiredDataForm {
    form: FormGroup;

    constructor(
        @Inject(FormBuilder) private _builder: FormBuilder
    ) {
        this.form = _builder.group({
            Token: ["", Validators.required]
        })
    }
}