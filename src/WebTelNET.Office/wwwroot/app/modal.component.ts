import { Component, OnInit, ViewChild, ViewContainerRef, Compiler, EventEmitter } from '@angular/core';
import { ModalDirective } from "ng2-bootstrap/ng2-bootstrap";

import { AppModule } from "./office-app.module";
import { ModalService } from "./shared/services/modal.service";
import { DynamicComponentMode, IDynamicComponent, IDynamicComponentSettings } from "./shared/models";


@Component({
    moduleId: module.id,
    selector: 'modal-control',
    templateUrl: 'modal-control.html'
})
export class ModalControlComponent implements OnInit {
    @ViewChild('modalContent', { read: ViewContainerRef }) modalContent: ViewContainerRef;
    @ViewChild('modal') modal: ModalDirective;

    onShow: EventEmitter<ModalDirective>;
    onHide: EventEmitter<ModalDirective>;

    constructor(
        private _modalService: ModalService,
        private _compiler: Compiler
    ) { 
        this.onShow = _modalService.onShow.subscribe((settings: IDynamicComponentSettings) => { this._onShow(settings); });
        this.onHide = _modalService.onHide.subscribe((result: boolean) => { this._onHide(result); });
    }

    ngOnInit() {

    }

    private _onShow(settings: IDynamicComponentSettings) {
        let onShown = this.modal.onShown.subscribe(() => {
            this._compiler.compileModuleAndAllComponentsAsync(AppModule)
                .then(factory => {
                    let compFactory = factory.componentFactories.find(x => x.selector === settings.component);
                    if (compFactory) {
                        let theComponent = this.modalContent.createComponent<IDynamicComponent>(compFactory, 0);

                        if (theComponent) {
                            theComponent.instance.init(settings);
                            if (theComponent.instance.destroy) {
                                theComponent.onDestroy(() => theComponent.instance.destroy());
                            }
                        }
                        return theComponent;
                    }
                })
                .catch(rej => { console.error(rej); });
            })
            
        this.modal.onHidden.subscribe(() => {
            this.modalContent.clear();
            onShown.unsubscribe();
        })

        this.modal.show();
    }

    private _onHide(result: boolean) {
        this.modal.hide();
    }
}