import { Component, OnInit, OnDestroy } from "@angular/core";
import { PBXService } from "../shared/services/pbx.service";
import { StorageService } from "@commonclient/services";


@Component({
    moduleId: module.id,
    templateUrl: "get-script-page.html"
})
export class GetScriptPageComponent implements OnInit, OnDestroy {
    private _keyName = "widgetId";

    widgetId: string = '';

    constructor(private _pbxService: PBXService, private _storageServcie: StorageService) {}

    ngOnInit(): void {
        var id = this._storageServcie.getItem(this._keyName);
        if (!id) {
            this._pbxService.getWidgetId()
                .then(response => {this.widgetId = response.data.WidgetId })
                .catch(error => console.error(error));
        } else {
            this.widgetId = id;
        }
    }

    ngOnDestroy(): void {
        this._storageServcie.setItem("widgetId", this.widgetId, true);
    }
}