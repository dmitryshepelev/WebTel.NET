import { EventEmitter, Output } from "@angular/core";

export interface ISubmitable {
    onSubmitingStart: EventEmitter<any>;
    onSubmitingEnd: EventEmitter<any>;
}

export class SubmitingComponent implements ISubmitable {
    private _submiting: boolean;

    @Output()
    onSubmitingStart = new EventEmitter<any>();
    @Output()
    onSubmitingEnd = new EventEmitter<any>();

    get submiting(): boolean {
        return this._submiting;
    }

    set submiting(value: boolean) {
        this._submiting = value;
        if (value) {
            this.onSubmitingStart.emit();
        } else {
            this.onSubmitingEnd.emit();
        }
    }
}