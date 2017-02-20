import { Component, Input } from "@angular/core";

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

    @Input() userService: UserServiceInfo;

    constructor() {
        this.userService = new UserServiceInfo();
    }

    get serviceCardClass(): string {
        return this._cardClasses[this.userService.status - 1];
    }

    activate() {

    }
}