import { Component, Input, OnChanges, SimpleChange } from "@angular/core";

@Component({
    moduleId: module.id,
    selector: "player",
    templateUrl: "player.html"
})
export class PlayerComponent implements OnChanges {
    private _player: any;

    isPlaying: boolean = false;
    isMuted: boolean = false;
    get isReady(): boolean { return this._player ? !!this._player.src : false; }

    @Input()
    href: string;

    constructor() {
        this._player = new Audio();
    }

    ngOnChanges(changes: { [propKey: string]: SimpleChange }): void {
        const hrefChange = changes["href"];
        if (hrefChange != null && !hrefChange.isFirstChange()) {
            console.log(changes);

            this.stop();
            this._player.src = hrefChange.currentValue;
            this.play();
        }
    }

    play() {
        if (this._player.src) {
            this._player.play();
            this.isPlaying = true;
        }
    }

    stop() {
        this.pause();
        this._player.currentTime = 0;
    }

    pause() {
        this._player.pause();
        this.isPlaying = false;
    }

    mute() {
        this._player.volume = new Number(this.isMuted);
        this.isMuted = !this.isMuted;
    }
}