import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsFormComponent } from "./statistics-form.component";
import { ResponseModel, StorageService } from "@commonclient/services";
import { AlertComponent, AlertType } from "@commonclient/controls";
import { CallModel } from "../shared/models";

import * as moment from "moment";


class StatisticsModel {
    public calls: Array<CallModel>;
    public startDate: Date;
    public endDate: Date;

    constructor() {
        this.startDate = new Date();
        this.endDate = new Date();
    }

    setData(startDate: Date, endDate: Date, calls: Array<CallModel>) {
        this.startDate = startDate;
        this.endDate = endDate;
        this.calls = calls;
    }
}

@Component({
    moduleId: module.id,
    templateUrl: "statistics-page.html"
})
export class StatisticsPageComponent implements OnInit, AfterViewInit, OnDestroy {
    private _itemName = "statisticsModel";

    model: StatisticsModel;
    shownFilters = false;

    @ViewChild(StatisticsFormComponent)
    statisticsFormComponent: StatisticsFormComponent;

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    constructor(private _pbxService: PBXService, private _storageService: StorageService) {
        this.model = new StatisticsModel();
    }

    toggleFilterPanel() {
        this.shownFilters = !this.shownFilters;
    }

    onFiltersFormSubmitSuccess(result: ResponseModel) {
        this.model.setData(this.statisticsFormComponent.model.start, this.statisticsFormComponent.model.end, result.data.calls);
        this.shownFilters = false;
    }

    onFiltersFormSubmitFailure(error: ResponseModel) {
        console.log(error);
        this.alertComponent.message = error.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
        this.shownFilters = false;
    }

    onGetCallRecordFailure(error: ResponseModel) {
        console.log(error);
        this.alertComponent.message = error.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }

    ngOnInit() {}

    ngAfterViewInit(): void {
        var cached = this._storageService.getItem(this._itemName);
        if (!cached) {
            this.statisticsFormComponent.onSubmit();
        } else {
            var model = <StatisticsModel>cached;
            this.model.setData(model.startDate, model.endDate, model.calls);
        }
    }

    ngOnDestroy(): void {
        this._storageService.setItem(this._itemName, this.model, true);
    }
}