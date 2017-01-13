import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsFormComponent } from "./statistics-form.component";
import { ResponseModel, StorageService } from "@commonclient/services";
import { AlertComponent, AlertType } from "@commonclient/controls";
import { CallModel, StatisticsModel } from "../shared/models";

import * as moment from "moment";


@Component({
    moduleId: module.id,
    templateUrl: "statistics-page.html"
})
export class StatisticsPageComponent implements OnInit, AfterViewInit, OnDestroy {
    private _itemName = "statisticsModel";

    model: StatisticsModel;
    shownFilters = false;
    callRecordHref: string;

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
        this.showErrorAlert(error);
        this.shownFilters = false;
    }

    onGetCallRecordSuccess(result: ResponseModel) {
        this.callRecordHref = result.data.Href;
    }

    onGetCallRecordFailure(error: ResponseModel) {
        this.showErrorAlert(error);
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

    private showErrorAlert(model: ResponseModel) {
        this.alertComponent.message = model.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }
}