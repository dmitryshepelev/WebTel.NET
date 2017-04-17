import { Component, OnInit, Input } from "@angular/core";
import { PBXService } from "../services/pbx.service";
import { BalanceModel } from "../models";
import { StorageService } from "@commonclient/services";


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
    private _itemName = "balanceData";

    model: BalanceModel = new BalanceModel();
    status: number;
    cardStatus = CardStatus;

    @Input() compactMode = false;

    constructor(
        private _pbxService: PBXService,
        private _storageService: StorageService
    ) {
        this.status = CardStatus.IsLoading;
    }

    ngOnInit(): void {
        const cached = this._storageService.getItem(this._itemName);
        if (!cached) {
            this.update();
        } else {
            this._setModelValue(cached as BalanceModel);
        }
    }

    update() {
        this.status = CardStatus.IsLoading;
        this._pbxService.getBalance()
            .then(response => {
                this._setModelValue(response.data.Balance as BalanceModel);
            })
            .catch(error => this.status = CardStatus.Error);
    }

    private _setModelValue(data: BalanceModel) {
        this.model = data;

        if (this.model.balance <= 0) {
            this.status = CardStatus.Danger;
        } else if (this.model.balance < 50) {
            this.status = CardStatus.Warning;
        } else {
            this.status = CardStatus.Save;
        }

        this._storageService.setItem(this._itemName, this.model, true);
    }
}