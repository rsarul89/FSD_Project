import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

import { Login, LoginResponse } from '../models/index';
import { AppSettings } from '../shared/index';
import { AlertService } from '../services/alert.service';


@Injectable()
export class AuthenticationService {

    private redirectUrl: string = '/';
    private loginUrl: string = '/login';

    constructor(private httpClient: Http, private appSettings: AppSettings, private alertService: AlertService) {
    }
    login(login: Login): Promise<LoginResponse> {
        let body = JSON.stringify(login);
        return this.httpClient.post("auth/authenticate", body)
            .toPromise()
            .then(response => response.json() as LoginResponse)
            .catch(this.handleError);
    }
    logout() {
        this.appSettings.removeUser();
    }
    getRedirectUrl(): string {
        return this.redirectUrl;
    }
    setRedirectUrl(url: string): void {
        this.redirectUrl = url;
    }
    getLoginUrl(): string {
        return this.loginUrl;
    }
    private handleError(error: any): Promise<any> {
        this.alertService.error(error.message, false);
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
} 