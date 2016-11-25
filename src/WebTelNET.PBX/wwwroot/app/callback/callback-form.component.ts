import { Component, Inject, EventEmitter, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { SubmitingComponent, ISubmitable } from "@commonclient/components";

import { CallbackModel } from "./callback-model";
import { PBXService } from "../shared/pbx.service";



@Component({
    moduleId: module.id,
    selector: "callback-form",
    templateUrl: "callback-form.html"
})
export class CallbackFormComponent extends SubmitingComponent {
    form: FormGroup;

    constructor(private pbxService: PBXService, @Inject(FormBuilder) builder: FormBuilder) {
        super();
        this.form = builder.group({
            from: ["", Validators.required],
            to: ["", Validators.required]
        });
        this.form.controls["from"].valueChanges.subscribe(data => console.log(data));
    }

    submit() {
        const model = new CallbackModel(this.form.controls["from"].value, this.form.controls["to"].value);
        this.startSubmiting();

    }
}