import { trigger, transition, style, animate, AnimationEntryMetadata } from "@angular/core";

export var inOut = trigger("InOut", [
    transition("void => *", [
        style({ opacity: 0 }),
        animate("300ms", style({ opacity: 1 }))
    ]),
    transition("* => void", [
        style({ opacity: 1 }),
        animate("300ms", style({ opacity: 0 }))
    ])
]);