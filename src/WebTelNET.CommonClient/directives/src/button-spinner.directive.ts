import { Directive, ElementRef, Input, OnChanges, SimpleChange, Renderer, AfterViewInit } from "@angular/core";

@Directive({
    selector: "[buttonSpinner]"
})
export class ButtonSpinnerDirective implements OnChanges, AfterViewInit {
    private _text: string;
    private _width: number;

    @Input("buttonSpinner") buttonSpinner: boolean;
    @Input("saveWidth") saveWidth: boolean = true;
    @Input() iconClass: string = "fa-circle-o-notch";

    constructor(private element: ElementRef, private renderer: Renderer) {}

    ngOnChanges(changes: { [propKey: string]: SimpleChange }): void {
        const change = changes["buttonSpinner"];
        if (change != null && !change.isFirstChange()) {
            this.setSpinner(change.currentValue);
        }
    }

    ngAfterViewInit(): void {
        this._text = this.element.nativeElement.innerHTML;
        this._width = this.element.nativeElement.offsetWidth;
    }

    private getTemplate() {
        return `<i class="fa ${this.iconClass} fa-spin fa-fw"></i>`;
    }

    private setSpinner(value: boolean) {
        var html: string;
        if (value) {
            html = this.getTemplate();
        } else {
            html = this._text;
        }

        if (this.saveWidth) {
            this.renderer.setElementStyle(this.element.nativeElement, "width", `${this._width}px`);
        }
        this.renderer.setElementProperty(this.element.nativeElement, "innerHTML", html);
    }
}