import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SubmitingComponent } from "./src/submiting.component";

export * from "./src/submiting.component";

@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
    ],
    exports: [
    ]
})
export default class ComponentsModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ComponentsModule,
            providers: []
        };
    }
}
