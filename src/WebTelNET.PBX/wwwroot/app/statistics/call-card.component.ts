import { Component, Input, Output, EventEmitter, OnInit } from "@angular/core";
import { CallModel, CallDispositoinType, CallType } from "../shared/models";
import { PBXService } from "../shared/services/pbx.service";
import { ResponseModel } from "@commonclient/services";


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

    @Output()
    onGetRecordFailure = new EventEmitter<ResponseModel>();

    constructor(private _pbxService: PBXService) {
    }

    getRecordLink() {
        this.isRecordLoading = true;
        this._pbxService.getCallRecordLink(this.call.pbxCallId)
            .then(response => console.log(response))
            .catch(error => this.onGetRecordFailure.emit(error))
            .then(() => this.isRecordLoading = false);
    }

    ngOnInit(): void {}
}