import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";

export * from "./src/alert.component";

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
  ],
  exports: [
  ]
})
export default class SampleModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SampleModule,
      providers: []
    };
  }
}
