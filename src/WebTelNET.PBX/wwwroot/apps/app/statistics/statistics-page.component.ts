import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsFormComponent } from "./statistics-form.component";
import { ResponseModel, StorageService } from "@commonclient/services";
import { AlertComponent, AlertType } from "@commonclient/controls";
import { CallModel, StatisticsModel } from "../shared/models";

import * as moment from "moment";


class NotificationConfigInfo {
    isConfigured: boolean;
    link: string;
}


@Component({
    moduleId: module.id,
    templateUrl: "statistics-page.html"
})
export class StatisticsPageComponent implements OnInit, AfterViewInit, OnDestroy {
    private _itemName = "statisticsModel";
    private _notificationConfigItemName: "notificationConfig";

    model: StatisticsModel;
    shownFilters = false;
    shownNotificationConfig = false;
    notificationConfigInfo: NotificationConfigInfo;
    callRecordHref: string;

    @ViewChild(StatisticsFormComponent)
    statisticsFormComponent: StatisticsFormComponent;

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    constructor(
        private _pbxService: PBXService,
        private _storageService: StorageService
    ) {
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

    ngOnInit() {
        let cached = this._storageService.getItem(this._notificationConfigItemName);
        if (cached) {
            this.initNotificationConfig(cached as NotificationConfigInfo);
        } else {
            this._pbxService.getNotificationConfigInfo()
                .then((response: ResponseModel) => {
                    this.initNotificationConfig(response.data.NotificationConfigInfo as NotificationConfigInfo);
                })
                .catch(error => { console.log(error);});
        }

    }

    ngAfterViewInit(): void {
        var cached = this._storageService.getItem(this._itemName);
        if (!cached) {
            this.statisticsFormComponent.onSubmit();
        } else {
            var model = cached as StatisticsModel;
            this.model.setData(model.startDate, model.endDate, model.calls);
        }
    }

    ngOnDestroy(): void {
        this._storageService.setItem(this._itemName, this.model, true);

    }

    notificationConfigured() {
        this._pbxService.setNotificationConfiguration()
            .then((response: ResponseModel) => {
                this.shownNotificationConfig = false;
            })
            .catch(error => {});
    }

    private showErrorAlert(model: ResponseModel) {
        this.alertComponent.message = model.message;
        this.alertComponent.type = AlertType.Error;
        this.alertComponent.show();
    }

    private initNotificationConfig(config: NotificationConfigInfo) {
        if (config.isConfigured) {
            this._storageService.setItem(this._notificationConfigItemName, config, true);
        } else {
            this.notificationConfigInfo = config;
            setTimeout(() => { this.shownNotificationConfig = true; }, 2000);
        }
    }
}