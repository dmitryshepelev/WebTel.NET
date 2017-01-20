import { Component, Inject, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from "@angular/forms";
import { SubmitingComponent } from "@commonclient/components";


@Component({
    moduleId: module.id,
    selector: "get-script-form",
    templateUrl: "get-script-form.html"
})
export class GetScriptFormComponent extends SubmitingComponent implements OnInit {
    form: FormGroup;

    constructor(@Inject(FormBuilder) builder: FormBuilder) {
        super();

        this.form = builder.group({
            counterNumber: ["", Validators.required]
        });
    }

    onSubmit() {
    }

    ngOnInit(): void {
    }
}