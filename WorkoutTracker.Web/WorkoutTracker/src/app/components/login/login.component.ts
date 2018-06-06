import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Login } from '../../models/index';
import { AlertService, AuthenticationService } from '../../services/index';
import { AppSettings } from '../../shared/index';

@Component({
    moduleId: module.id,
    changeDetection: ChangeDetectionStrategy.Default,
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
    model = new Login("", "");
    loading = false;
    returnUrl: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private alertService: AlertService,
        private authService: AuthenticationService,
        private appSettings: AppSettings) { }

    ngOnInit() {
        // reset login status
        this.authService.logout();

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    login() {
        this.loading = true;
        this.authService.login(this.model)
            .then(
            response => {
                this.appSettings.setUser(response);
                this.router.navigate([this.returnUrl]);
            }).catch(
            error => {
                this.alertService.error(error);
            });
    }

}