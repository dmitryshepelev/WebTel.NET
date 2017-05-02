import { Component, Inject, OnInit, ElementRef, Compiler, ViewContainerRef, ViewChild } from "@angular/core";
import { ResponseModel } from "@commonclient/services";

import { OfficeService, IOfficeService } from "../shared/services/office.service";

import { UserServiceInfo } from "../shared/models";

@Component({
    moduleId: module.id,
    selector: "services-page",
    templateUrl: "services-page.html"
})
export class ServicesPageComponent implements OnInit {

    services: Array<UserServiceInfo>;

    constructor(
        @Inject(OfficeService) private _officeService: IOfficeService
    ) {
        this.services = [];
    }

    ngOnInit(): void {
        this._officeService.getUserServices()
            .then((result: ResponseModel) => {
                this.services = result.data.services as Array<UserServiceInfo>;
            })
            .catch(error => {
                console.log(error);
            });
    }
}