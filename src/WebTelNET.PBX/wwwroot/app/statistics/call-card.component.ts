import { Component, Input, OnInit } from "@angular/core";
import { CallModel, CallDispositoinType, CallType } from "../shared/models";
import { PBXService } from "../shared/services/pbx.service";


@Component({
    moduleId: module.id,
    selector: "call-card",
    templateUrl: "call-card.html"
})
export class CallCardComponent implements OnInit {
    dispositionType = CallDispositoinType;
    callType = CallType;
    isRecordLoading: boolean = false;

    @Input()
    call: CallModel;

    constructor(private _pbxService: PBXService) {
    }

    getRecordLink() {
        this.isRecordLoading = true;
        this._pbxService.getCallRecordLink(this.call.pbxCallId)
            .then(response => console.log(response))
            .catch(error => console.log(error))
            .then(() => this.isRecordLoading = false);
    }

    ngOnInit(): void {}
}