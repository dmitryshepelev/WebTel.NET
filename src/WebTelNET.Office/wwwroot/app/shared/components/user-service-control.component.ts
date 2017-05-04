import { Component, Input, Inject, ViewChild, ElementRef, OnDestroy, EventEmitter } from "@angular/core";
import { ResponseModel } from "@commonclient/services";
import { Sidebar } from "ng-sidebar";

import { OfficeService, IOfficeService } from "../services/office.service";
import { ModalService } from "../services/modal.service";
import { UserServiceInfo, UserServiceStatus, IServiceStatus, DynamicComponentMode, IDynamicComponent, IDynamicComponentSettings } from "../models";


@Component({
    moduleId: module.id,
    selector: "user-service-control",
    templateUrl: "user-service-control.html"
})
export class UserServiceControlComponent implements OnDestroy {
    private _cardClasses = [
        "card-outline-secondary",
        "card-outline-success"
    ];
    private _onServiceStatusChangedSubscriber: EventEmitter<IServiceStatus>;

    cardActionExecuting: boolean;

    @Input() userService: UserServiceInfo;

    constructor(
        private _modalService: ModalService,
        @Inject(OfficeService) private _officeService: IOfficeService
    ) {
        this.userService = new UserServiceInfo();

        this.cardActionExecuting = false;

        this._onServiceStatusChangedSubscriber = this._officeService.onServiceStatusChanged.subscribe((res: IServiceStatus) => { this._onServiceStatusChanged(res) })
    }

    ngOnDestroy(): void {
        this._onServiceStatusChangedSubscriber.unsubscribe();
    }

    get serviceCardClass(): string {
        return this._cardClasses[this.userService.status - 1];
    }

    activate() {
        if (this.userService.requireActivationData) {
            this._openModal(DynamicComponentMode.NEW);
        } else {
            this._activateService();
        }
    }

    openSettings() {
        this._openModal(DynamicComponentMode.EDIT);
    }

    private _updateService(status: IServiceStatus) {
        if (this.userService.serviceType === status.serviceType) {
            switch (status.status) {
                case UserServiceStatus.Activated:
                    this._activate();
                    break;
                default:
                    break;
            }
        }
        
    }

    private _activateService() {
        this.cardActionExecuting = true;
        this._officeService.activateService(this.userService.serviceType)
            .then((response: ResponseModel) => {
                this._updateService({ serviceType: this.userService.serviceType, status: UserServiceStatus.Activated });
            })
            .catch(error => {
                console.log(error);
            })
            .then(() => {
                this.cardActionExecuting = false;
            });
    }

    private _onServiceStatusChanged(status: IServiceStatus) {
        this._modalService.hide();
        this._updateService(status);
    }

    private _openModal(mode: DynamicComponentMode) {
        let arrNames = this.userService.serviceType.match(/[A-Z][a-z]+/g);
        let componentSelector = `${(arrNames == null ? this.userService.serviceType : arrNames.join("-")).toLowerCase()}-required-data-form`;
        this._modalService.show({ component: componentSelector, mode: mode, title: "Настройки" });
    }

    private _activate() {
        this.userService.activationDateTime = new Date();
        this.userService.status = UserServiceStatus.Activated;
    }
}