import { Component, OnInit, Input } from "@angular/core";
import { PBXService } from "../services/pbx.service";
import { BalanceModel } from "../models";


enum CardStatus {
    IsLoading = 1,
    Error,
    Save,
    Warning,
    Danger,
}

@Component({
    moduleId: module.id,
    selector: "balance-card",
    templateUrl: "balance-card.html"
})
export class BalanceCardComponent implements OnInit {
    model: BalanceModel = new BalanceModel();
    status: number;
    cardStatus = CardStatus;

    constructor(private _pbxService: PBXService) {
        this.status = CardStatus.IsLoading;
    }

    ngOnInit(): void {
        this.update();
    }

    update() {
        this.status = CardStatus.IsLoading;
        this._pbxService.getBalance()
            .then(response => {
                this.model = <BalanceModel>response.data.Balance;

                if (this.model.balance <= 0) {
                    this.status = CardStatus.Danger;
                } else if (this.model.balance < 50) {
                    this.status = CardStatus.Warning;
                } else {
                    this.status = CardStatus.Save;
                }
            })
            .catch(error => this.status = CardStatus.Error);
    }
}