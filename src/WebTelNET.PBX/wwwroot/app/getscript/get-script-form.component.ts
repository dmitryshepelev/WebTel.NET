import { Component, Inject, OnInit, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from "@angular/forms";
import { SubmitingComponent } from "@commonclient/components";
import { ResponseModel, ResponseType } from "@commonclient/services";


@Component({
    moduleId: module.id,
    selector: "get-script-form",
    templateUrl: "get-script-form.html"
})
export class GetScriptFormComponent extends SubmitingComponent implements OnInit {
    form: FormGroup;

    @Output()
    onSubmitSuccess = new EventEmitter<ResponseModel>();

    constructor(@Inject(FormBuilder) builder: FormBuilder) {
        super();

        this.form = builder.group({
            counterNumber: ["", Validators.required]
        });
    }

    onSubmit() {
        var model = new ResponseModel();
        model.type = ResponseType.Success;
        model.data = {
            CounterNumber: this.form.controls["counterNumber"].value
        }
        this.onSubmitSuccess.emit(model);
    }

    ngOnInit(): void {
    }
}