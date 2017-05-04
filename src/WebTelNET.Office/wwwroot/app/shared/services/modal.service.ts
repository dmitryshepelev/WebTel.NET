import { Injectable, ViewChild, ViewContainerRef, Output, EventEmitter } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ModalDirective } from "ng2-bootstrap/ng2-bootstrap";

import { IModalSettings } from "../models";

@Injectable()
export class ModalService {
    
    @Output() onShow = new EventEmitter<IModalSettings>();
    @Output() onHide = new EventEmitter<boolean>();

    constructor() {
        
    }

    show(settings: IModalSettings): void {
        this.onShow.emit(settings);
    }

    hide(): void {
        this.onHide.emit(true);
    }
}