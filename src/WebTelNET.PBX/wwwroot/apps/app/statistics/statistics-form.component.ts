import { Component, Inject, OnInit, Input, ChangeDetectorRef, AfterViewInit, Output, EventEmitter  } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from "@angular/forms";
import { ResponseModel } from "@commonclient/services";
import { SubmitingComponent } from "@commonclient/components";
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsParamsModel } from "../shared/models";

import * as moment from "moment";
import "moment_ru";

@Component({
    moduleId: module.id,
    selector: "statistics-form",
    templateUrl: "statistics-form.html"
})
export class StatisticsFormComponent extends SubmitingComponent implements OnInit, AfterViewInit {
    @Input()
    startDate: Date;
    @Input()
    endDate: Date;

    form: FormGroup;
    model: StatisticsParamsModel = new StatisticsParamsModel();

    @Output()
    onFormSubmitSuccess = new EventEmitter<ResponseModel>();
    @Output()
    onFormSubmitFailure = new EventEmitter<ResponseModel>();

    constructor(
        private _cdr: ChangeDetectorRef,
        @Inject(FormBuilder) private _builder: FormBuilder,
        private _pbxService: PBXService
    ) {
        super();
        this.form = this._builder.group({

        });
    }

    ngOnInit(): void {
        moment.locale("ru");
    }

    ngAfterViewInit(): void {
        //this._cdr.detectChanges();
    }

    onSubmit() {
        this.startSubmiting();
        this._pbxService.getStatistics(this._correctDateTime(this.model.start), this._correctDateTime(this.model.end))
            .then(response => this.onFormSubmitSuccess.emit(response))
            .catch(error => this.onFormSubmitFailure.emit(error))
            .then(() => { this.endSubmiting(); });
    }

    private _correctDateTime(dateTime: Date): Date {
        var momentDate = moment(dateTime);
        var offset = momentDate.utcOffset();
        momentDate.add("m", offset);
        return momentDate.toDate();
    }
}