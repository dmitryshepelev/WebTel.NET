import { Component, OnInit } from "@angular/core";
import { StorageService } from "@commonclient/services";


@Component({
    moduleId: module.id,
    templateUrl: "callback-page.html"
})
export class CallbackPageComponent implements OnInit {
    constructor(private storageService: StorageService) { }

    ngOnInit(): void {
        this.storageService.setItem("aaaa", true);
        console.log(this.storageService.getItem("aaaa"));
    }
}