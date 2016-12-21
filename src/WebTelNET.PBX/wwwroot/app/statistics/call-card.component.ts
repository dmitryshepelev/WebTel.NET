import { Component, Input, OnInit } from "@angular/core";
import { CallModel, CallDispositoinType, CallType } from "../shared/models";



@Component({
    moduleId: module.id,
    selector: "call-card",
    templateUrl: "call-card.html"
})
export class CallCardComponent implements OnInit {
    dispositionType = CallDispositoinType;
    callType = CallType;


    @Input()
    call: CallModel;

    constructor() {

    }

    ngOnInit(): void {}
}