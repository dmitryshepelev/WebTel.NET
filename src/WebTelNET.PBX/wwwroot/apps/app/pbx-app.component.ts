import { Component } from '@angular/core';
import { TranslateService } from "ng2-translate";


@Component({
    moduleId: module.id,
    selector: 'pbx-app-root',
    templateUrl: 'pbx-app.html'
})
export class PBXAppComponent {

    shownSidePanel: boolean;

    constructor(private _translateService: TranslateService) {
        this._translateService.setDefaultLang("ru");
        this._translateService.use("ru");

        this.shownSidePanel = false;
    }
}