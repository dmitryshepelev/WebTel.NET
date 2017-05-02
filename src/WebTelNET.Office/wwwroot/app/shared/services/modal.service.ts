import { Injectable, ViewChild, ViewContainerRef, Output, EventEmitter } from '@angular/core';
import { Http, Response } from '@angular/http';
import { ModalDirective } from "ng2-bootstrap/ng2-bootstrap";

import { DynamicComponentMode, IDynamicComponent, IDynamicComponentSettings } from "../models";

@Injectable()
export class ModalService {
    
    @Output() onShow = new EventEmitter<IDynamicComponentSettings>();
    @Output() onHide = new EventEmitter<boolean>();

    constructor() {
        
    }

    show(settings: IDynamicComponentSettings) {
        this.onShow.emit(settings);
    }

    hide() {
        this.onHide.emit(true);
    }
}