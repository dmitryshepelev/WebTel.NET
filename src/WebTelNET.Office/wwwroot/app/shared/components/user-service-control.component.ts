import { Component, Input, Inject } from "@angular/core";
import { ResponseModel } from "@commonclient/services"

import { OfficeService, IOfficeService } from "../services/office.service";

import { UserServiceInfo } from "../models"


@Component({
    moduleId: module.id,
    selector: "user-service-control",
    templateUrl: "user-service-control.html"
})
export class UserServiceControlComponent {
    private _cardClasses = [
        "card-outline-secondary",
        "card-outline-success"
    ];

    cardActionExecuting: boolean;

    @Input() userService: UserServiceInfo;

    constructor(
        @Inject(OfficeService) private _officeService: IOfficeService
    ) {
        this.userService = new UserServiceInfo();

        this.cardActionExecuting = false;
    }

    get serviceCardClass(): string {
        return this._cardClasses[this.userService.status - 1];
    }

    activate() {
        this.cardActionExecuting = true;
        this._officeService.activateService(this.userService.serviceType)
            .then((response: ResponseModel) => {
                    this.userService.activationDateTime = new Date();
                    this.userService.status = 2;
                })
            .catch(error => {
                    console.log(error);
                })
            .then(() => {
                    this.cardActionExecuting = false;
                });
    }
}