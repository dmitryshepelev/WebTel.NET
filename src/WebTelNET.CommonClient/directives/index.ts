import { NgModule, ModuleWithProviders } from "@angular/core";
import { ButtonSpinnerDirective } from "./src/button-spinner.directive";

export * from "./src/button-spinner.directive"


@NgModule({
    imports: [
    ],
    declarations: [
        ButtonSpinnerDirective
    ],
    exports: [
    ]
})
export class DirectivesModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: DirectivesModule,
            providers: []
        };
    }
}
