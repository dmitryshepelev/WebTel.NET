import { Component, OnInit, ViewChild } from '@angular/core';
import { PBXService } from "../shared/services/pbx.service";
import { StatisticsFormComponent } from "./statistics-form.component";


@Component({
    moduleId: module.id,
    templateUrl: "statistics-page.html"
})
export class StatisticsPageComponent implements OnInit {
    pbxStatistics: any;
    overallStatistics: any;

    shownFilters = false;
    startDate = new Date();
    endDate: Date;

    @ViewChild(StatisticsFormComponent)
    statisticsFormComponent: StatisticsFormComponent;

    constructor(private _pbxService: PBXService) { }

    toggleFilterPanel() {
        this.shownFilters = !this.shownFilters;
        //console.log(this.shownFilters);
        //this.statisticsFormComponent.initDate = new Date();
    }

    ngOnInit() {
        this._pbxService.getPBXStatistics()
            .then(response => { console.log(response);
                this.pbxStatistics = response.data.Stats;
            })
            .catch(error => console.log(error));

        this._pbxService.getOverallStatistics()
            .then(response => {
                console.log(response);
                this.overallStatistics = response.data.Stats;
            })
            .catch(error => console.log(error));
    }
}