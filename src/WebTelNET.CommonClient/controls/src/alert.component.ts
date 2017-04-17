import { Component, OnInit } from "@angular/core";
import { StorageService } from "@commonclient/services";

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
    selector: "alert",
    template: 
    `
        <div *ngIf="isShown" class="row alert alert-{{ alertClass }}" role="alert">
            <button type="button" class="close" aria-label="Close" about=""(click)="hide()">
                <span aria-hidden="true">&times;</span>
            </button>
            <h4 *ngIf="title" class="alert-heading">{{ title }}</h4>
            <span>{{ message }}</span>
        </div>
    `
})
export class AlertComponent implements OnInit {
    private classes: string[] = ["success", "danger", "warning", "info"];
    private model: AlertModel;

    protected alertClass: string;
    protected isShown = false;

    constructor(private storageService: StorageService) {
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

    ngOnInit(): void {
        var alert = this.storageService.getItem("alert") as AlertModel;
        if (alert != null) {
            this.message = alert.message;
            this.title = alert.title;
            this.type = alert.type;

            this.show();
        }
    }
}