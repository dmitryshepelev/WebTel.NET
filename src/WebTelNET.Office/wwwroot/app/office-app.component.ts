import { Component } from '@angular/core';

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