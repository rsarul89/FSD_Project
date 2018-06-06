import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppSettings } from '../../shared/index';

@Component({
    moduleId: module.id,
    selector: 'top-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
    public userName: string = '';

    constructor(private router: Router, private appSettings: AppSettings) {
    }

    ngOnInit() {
        this.appSettings.currentUname.subscribe(uname => this.userName = uname);
    }
    showLogout() {
        let user = JSON.parse(localStorage.getItem('loggedUser'));
        if (user != null) {
            return true;
        }
        else {
            return false;
        }
    }
    showLogin() {
        let user = JSON.parse(localStorage.getItem('loggedUser'));
        if (user != null) {
            return false;
        }
        else {
            return true;
        }
    }
    Logout() {
        localStorage.removeItem('loggedUser');
        this.userName = '';
        this.router.navigate(['login']);
    }
}
