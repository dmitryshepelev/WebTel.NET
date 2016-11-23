import { Component, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
    moduleId: module.id,
    selector: "callback-form",
    templateUrl: "callback-form.html"
})
export class CallbackFormComponent {
    form: FormGroup;

    constructor(@Inject(FormBuilder) builder: FormBuilder) {
        this.form = builder.group({
            from: ["", Validators.required],
            to: ["", Validators.required]
        });
    }

    submit() {
        
    }
}