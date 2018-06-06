import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Response } from '@angular/http';

import { LoginResponse } from '../models/index';

@Injectable()
export class AppSettings {

    public API_ENDPOINT = 'http://localhost:50499/api/';

    private unameSource = new BehaviorSubject("");
    currentUname = this.unameSource.asObservable();
    private tokenSource = new BehaviorSubject("");
    currentToken = this.tokenSource.asObservable();

    constructor() {
        let user = JSON.parse(localStorage.getItem('loggedUser'));
        if (user != null)
        {
            this.unameSource.next(user.UserName);
            this.tokenSource.next(user.Token);
        }
    }

    setUser(response:LoginResponse) {
        localStorage.setItem('loggedUser', JSON.stringify(response));
        this.unameSource.next(response.UserName);
        this.tokenSource.next(response.Token);
    }

   getLoggedUser(): LoginResponse{
       let user = JSON.parse(localStorage.getItem('loggedUser'));
       return user;
   }

    getToken() {
        let user = JSON.parse(localStorage.getItem('loggedUser'));
        let token = user.Token;
        if (token)
            return token;
        else
            return '';
    }
    removeUser() {
       localStorage.removeItem('loggedUser');
    }
    //extractData(res: HttpResponse<Object>) {
    //    var array = new Array();
    //    var key, count = 0;
    //    for (key in res.body) {
    //        array.push(res.body[count++]);
    //    }
    //    return array;
    //}

    extractData(res: Response) {
        let body = res.json();
        return body || {};
    }
}