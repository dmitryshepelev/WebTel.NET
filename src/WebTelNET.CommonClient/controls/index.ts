import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";

export * from "./src/alert.component";
export * from "./src/player.component";

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
  ],
  exports: [
  ]
})
export default class ControlsModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: ControlsModule,
      providers: []
    };
  }
}
