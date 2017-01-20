import { Component, Input, OnChanges, SimpleChange, OnDestroy } from "@angular/core";

@Component({
    selector: "player",
    template: `
        <div>
            <button class="btn btn-primary btn-sm" *ngIf="!isPlaying" [disabled]="!isReady" (click)="play()"><i class="fa fa-play"></i></button>
            <button class="btn btn-outline-primary btn-sm" *ngIf="isPlaying" [disabled]="!isReady" (click)="pause()"><i class="fa fa-pause"></i></button>
            <button class="btn btn-primary btn-sm" [disabled]="!isReady" (click)="stop()"><i class="fa fa-stop"></i></button>
            <button class="btn btn-link btn-sm" [disabled]="!isReady" (click)="mute()"><i class="fa" [class.fa-volume-up]="!isMuted" [class.fa-volume-off]="isMuted"></i></button>
        </div>
    `
})
export class PlayerComponent implements OnChanges, OnDestroy {
    private _player: any;

    isPlaying: boolean = false;
    isMuted: boolean = false;
    get isReady(): boolean { return this._player ? !!this._player.src : false; }

    @Input()
    href: string;
    @Input()
    autoPlay: boolean = false;

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

    ngOnDestroy(): void {
        this.stop();
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