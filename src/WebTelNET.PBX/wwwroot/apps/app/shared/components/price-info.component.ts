import { Component, Input, OnInit } from "@angular/core";


export class PriceInfoModel {
    public prefix: string;
    public description: string;
    public price: number;
    public currency: string;
}


@Component({
    moduleId: module.id,
    selector: "price-info",
    templateUrl: "price-info.html"
})
export class PriceInfoComponent implements OnInit {
    @Input() model: PriceInfoModel;

    ngOnInit(): void {

    }
}