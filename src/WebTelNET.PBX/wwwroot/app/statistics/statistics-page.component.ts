import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsFormComponent } from "./statistics-form.component";
import { ResponseModel } from "@commonclient/services";

import * as moment from "moment";

@Component({
    moduleId: module.id,
    templateUrl: "statistics-page.html"
})
export class StatisticsPageComponent implements OnInit, AfterViewInit {
    pbxStatistics: any;
    overallStatistics: any;

    shownFilters = false;
    startDate = new Date();
    endDate = new Date();
    durationString: string;

    @ViewChild(StatisticsFormComponent)
    statisticsFormComponent: StatisticsFormComponent;

    constructor(private _pbxService: PBXService) { }

    toggleFilterPanel() {
        this.shownFilters = !this.shownFilters;
    }

    onFiltersFormSubmitSuccess(result: ResponseModel) {
        this.startDate = this.statisticsFormComponent.model.start;
        this.endDate = this.statisticsFormComponent.model.end;

        this.pbxStatistics = result.data[0].data.Stats;
        this.overallStatistics = result.data[1].data.Stats;
    }

    ngOnInit() {
    //    this._pbxService.getStatistics()
    //        .then(response => console.log(response))
    //        .catch(error => console.log(error));
    }

    ngAfterViewInit(): void {
        this.statisticsFormComponent.onSubmit();
    }
}