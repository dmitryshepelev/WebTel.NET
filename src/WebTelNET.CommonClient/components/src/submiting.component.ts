import { EventEmitter, Output } from "@angular/core";

export interface ISubmitable {
    onSubmitingStart: EventEmitter<any>;
    onSubmitingEnd: EventEmitter<any>;
}

export abstract class SubmitingComponent {
    private _submiting: boolean;

    startSubmiting() {
        this._submiting = true;
    }
    endSubmiting() {
        this._submiting = false;
    }

    get submiting(): boolean { return this._submiting; }
    set submiting(value: boolean) {throw new Error("Setting isn't allowed") }
}