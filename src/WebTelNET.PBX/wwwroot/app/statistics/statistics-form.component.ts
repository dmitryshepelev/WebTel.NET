﻿import { Component, Inject, OnInit, Input, ChangeDetectorRef, AfterViewInit, Output, EventEmitter  } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from "@angular/forms";
import { ResponseModel } from "@commonclient/services";
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsParamsModel } from "../shared/models";

import * as moment from "moment";
import "moment_ru";

@Component({
    moduleId: module.id,
    selector: "statistics-form",
    templateUrl: "statistics-form.html"
})
export class StatisticsFormComponent implements OnInit, AfterViewInit {
    @Input()
    startDate: Date;
    @Input()
    endDate: Date;

    form: FormGroup;
    model: StatisticsParamsModel = new StatisticsParamsModel();

    @Output()
    onFormSubmitSuccess = new EventEmitter<ResponseModel>();

    constructor(
        private _cdr: ChangeDetectorRef,
        @Inject(FormBuilder) private _builder: FormBuilder,
        private _pbxService: PBXService
    ) {
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
        this._pbxService.getStatistics(this.model.start, this.model.end)
            .then(response => this.onFormSubmitSuccess.emit(response))
            .catch(error => console.log(error));
    }
}