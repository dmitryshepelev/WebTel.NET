import { trigger, transition, style, animate, AnimationEntryMetadata, state } from "@angular/core";

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

export var flyInOutUpDown = trigger("FlyInOutUpDown",
    [
        state('in', style({ transform: 'translateY(0)' })),
        transition('void => *', [
            style({ transform: 'translateY(-100%)' }),
            animate(500)
        ]),
        transition('* => void', [
            animate(500, style({ transform: 'translateY(-100%)' }))
        ])
    ])