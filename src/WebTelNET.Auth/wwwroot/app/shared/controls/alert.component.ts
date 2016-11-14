import { Component } from "@angular/core";

export enum AlertType {
    Success,
    Error,
    Warning,
    Info
}

export class AlertModel {
    public type: AlertType;
    public message: string;
    public title: string;
}

@Component({
    moduleId: module.id,
    selector: "alert",
    templateUrl: "alert.html"
})
export class AlertComponent {
    private classes: string[] = ["success", "danger", "warning", "info"];
    private model: AlertModel;

    protected alertClass: string;
    protected isShown = false;

    constructor() {
        this.model = new AlertModel();
    }

    get type(): AlertType { return this.model.type; }
    set type(value: AlertType) {
        this.model.type = value;
        this.alertClass = this.classes[this.model.type];
    }

    get message(): string { return this.model.message; }
    set message(value: string) { this.model.message = value; }

    get title(): string { return this.model.title; }
    set title(value: string) { this.model.title = value }

    show(): void {
        this.isShown = true;;
    }

    hide(): void {
        this.isShown = false;
    }
}