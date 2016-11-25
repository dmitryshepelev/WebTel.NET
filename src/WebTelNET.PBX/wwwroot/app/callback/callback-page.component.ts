import { Component, OnInit, ViewChild } from "@angular/core";
import { StorageService } from "@commonclient/services";
import { AlertComponent, AlertModel, AlertType } from "@commonclient/controls";


@Component({
    moduleId: module.id,
    templateUrl: "callback-page.html"
})
export class CallbackPageComponent implements OnInit {
    constructor(private storageService: StorageService) {
    }

    @ViewChild(AlertComponent)
    alertComponent: AlertComponent;

    ngOnInit(): void {

    }
}