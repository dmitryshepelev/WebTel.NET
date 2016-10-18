import { Component } from "@angular/core";

export enum AlertType {
    Success,
    Error,
    Warning,
    Info
}

@Component({
    moduleId: module.id,
    selector: "alert",
    templateUrl: "alert.html"
})
export class AlertComponent {
    private classes: string[] = ["success", "danger", "warning", "info"];

    protected alertClass: string;
    protected isShown = false;

    type: AlertType;
    message: string;
    title: string;

    private setClass(): void {
        this.alertClass = this.classes[this.type];
    }

    show(): void {
        this.setClass();
        this.isShown = true;;
    }

    hide(): void {
        this.isShown = false;
    }
}