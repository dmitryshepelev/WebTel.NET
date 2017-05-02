import { Component, ViewChild, ViewContainerRef } from '@angular/core';
import { ModalDirective } from "ng2-bootstrap/ng2-bootstrap";

@Component({
    moduleId: module.id,
    selector: 'office-app-root',
    templateUrl: 'office-app.html'
})
export class OfficeAppComponent {
    shownSidePanel: boolean;

    constructor() {
        this.shownSidePanel = false;
    }
}