import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StorageService } from "./src/storage.service";

export * from "./src/service";
export * from "./src/storage.service"


@NgModule({
    imports: [
        CommonModule
    ],
    declarations: [
    ],
    exports: [
    ]
})
export class ServicesModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: ServicesModule,
            providers: [StorageService]
        };
    }
}
