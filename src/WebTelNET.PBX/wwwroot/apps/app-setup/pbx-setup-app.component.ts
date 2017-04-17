import { Component } from '@angular/core';
import { TranslateService } from "ng2-translate";


@Component({
    moduleId: module.id,
    selector: 'pbx-setup-app-root',
    templateUrl: 'pbx-setup-app.html'
})
export class PBXSetupAppComponent {
    constructor(private _translateService: TranslateService) {
        this._translateService.setDefaultLang("ru");
        this._translateService.use("ru");
    }
}

