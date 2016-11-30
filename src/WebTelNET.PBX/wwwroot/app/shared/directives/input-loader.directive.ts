import { Directive, ElementRef, Input, Renderer } from "@angular/core";

@Directive({ selector: "[inputLoader]" })
export class InputLoaderDirective {
    @Input() flag: boolean;

    constructor(private element: ElementRef, private renderer: Renderer) {
        console.log(element);
        console.log(renderer);
        console.log(this.flag);
    }
}