export class CallbackModel {
    constructor(from: string, to: string) {
        this.from = from;
        this.to = to;
    }

    public from: string;
    public to: string;
}

export class StatisticsParamsModel {
    constructor() {

    }

    public start: Date;
    public end: Date;
}