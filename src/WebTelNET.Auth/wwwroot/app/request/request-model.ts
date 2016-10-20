export class RequestModel {
    constructor(login: string, email: string) {
        this.login = login;
        this.email = email;
    }

    public login: string;
    public email: string;
}