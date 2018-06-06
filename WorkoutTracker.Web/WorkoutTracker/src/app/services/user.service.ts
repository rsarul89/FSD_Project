import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

import { Register, User } from '../models/index';
import { AppSettings } from '../shared/index';
import { AlertService } from '../services/alert.service'

@Injectable()
export class UserService {

    constructor(private httpClient: Http, private appSettings: AppSettings, private alertService: AlertService) {
    }

    register(reg: Register): Promise<User> {
        let body = JSON.stringify(reg);
        return this.httpClient.post("user/register", body)
            .toPromise()
            .then(response => response.json() as User)
            .catch(this.handleError);
    }
    private handleError(error: any): Promise<any> {
        this.alertService.error(error.message, false);
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}