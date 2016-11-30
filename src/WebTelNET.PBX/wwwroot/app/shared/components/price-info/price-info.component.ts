import { Component } from "@angular/core";

import { PriceInfoModel } from "./price-info.model";


@Component({
    moduleId: module.id,
    selector: "price-info",
    templateUrl: "price-info.html"
})
export class PriceInfoComponent {
    model: PriceInfoModel
}