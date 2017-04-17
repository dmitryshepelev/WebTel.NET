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

export class CallModel {
    constructor() {}

    public callType: number;
    public callStart: Date;
    public pbxCallId: string;
    public caller: string;
    public destination: string;
    public internal: string;
    public duration: number;
    public dispositionType: number;
    public statusCode: number;
    public isRecorded: boolean;
    public callIdWithRecord: string;
}

export enum CallDispositoinType {
    Answered = 1,
    Busy,
    Cancel,
    NoAnswer,
    Failed,
    NoMoney,
    UnallocatedNumber,
    NoLimit,
    NoDayLimit,
    LineLimit,
    NoMoneyNoLimit
}

export enum CallType {
    Incoming = 3,
    Outgoing = 5
}

export class BalanceModel {
    public balance: number;
    public currency: string;
}

export class StatisticsModel {
    public calls: Array<CallModel>;
    public startDate: Date;
    public endDate: Date;

    constructor() {
        this.startDate = new Date();
        this.endDate = new Date();
    }

    setData(startDate: Date, endDate: Date, calls: Array<CallModel>) {
        this.startDate = startDate;
        this.endDate = endDate;
        this.calls = calls;
    }
}