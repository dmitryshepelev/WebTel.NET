import { Component, Input, Inject } from "@angular/core";
import { ResponseModel } from "@commonclient/services"

import { OfficeService, IOfficeService } from "../services/office.service";

import { UserServiceInfo, UserServiceStatus } from "../models"


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
    showDataForm: boolean;

    constructor(
        @Inject(OfficeService) private _officeService: IOfficeService
    ) {
        this.userService = new UserServiceInfo();
        this.showDataForm = false;

        this.cardActionExecuting = false;
    }

    private _updateService() {
        this.userService.activationDateTime = new Date();
        this.userService.status = UserServiceStatus.Activated;
    }

    private _activateService() {
        this.cardActionExecuting = true;
        this._officeService.activateService(this.userService.serviceType)
            .then((response: ResponseModel) => {
                this._updateService();
            })
            .catch(error => {
                console.log(error);
            })
            .then(() => {
                this.cardActionExecuting = false;
            });
    }

    get serviceCardClass(): string {
        return this._cardClasses[this.userService.status - 1];
    }

    activate() {
        if (this.userService.requireActivationData) {
            this.showDataForm = true;
        } else {
            this._activateService();
        }
    }

    onServiceActivated(value: any) {
        this.showDataForm = false;
        this._updateService();
    }

}