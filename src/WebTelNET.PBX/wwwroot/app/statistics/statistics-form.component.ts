import { Component, Inject, OnInit, Input, ChangeDetectorRef, AfterViewInit  } from "@angular/core";
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from "@angular/forms";

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

    constructor(private _cdr: ChangeDetectorRef, @Inject(FormBuilder) builder: FormBuilder) {
        this.form = builder.group({

        });
    }

    ngOnInit(): void {
        moment.locale("ru");
    }

    ngAfterViewInit(): void {
        this._cdr.detectChanges();
    }
}