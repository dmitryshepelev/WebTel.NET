import { Injectable } from "@angular/core";

class StorageItemMap {
    [key: string]: any;
}

@Injectable()
export class StorageService {
    private storage: StorageItemMap = {};

    getItem(key: string): any {
        return this.storage[key];
    }

    setItem(key: string, value: any, overwrite: boolean = false): void {
        if (!overwrite && this.getItem(key)) {
            throw new Error(`The key ${key} is already used.`);
        }
        this.storage[key] = value;
    }

    deleteItem(key: string): void {
        delete this.storage[key];
    }
}