﻿export class LoginModel {
    constructor(login: string, password: string) {
        this.login = login;
        this.password = password;
    }

    public login: string;
    public password: string;
}