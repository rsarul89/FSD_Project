import { Component, ChangeDetectionStrategy } from '@angular/core';
import { Router } from '@angular/router';

import { Register } from '../../models/index';
import { AlertService, UserService } from '../../services/index';

@Component({
    moduleId: module.id,
    changeDetection: ChangeDetectionStrategy.Default,
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent {
    model = new Register("", "");
    loading = false;

    constructor(
        private router: Router,
        private alertService: AlertService,
        private userService: UserService) { }

    register() {

        this.userService.register(this.model)
            .then(
            response => {
                if (response != undefined || response != null) {
                    this.alertService.success(`Hi ${response.user_name} you are successfully registered`, true);
                    this.router.navigate(['login']);
                }
                else {
                    this.alertService.error('User already exists with the given user name, please provide different user name', false);
                }
            }).catch(
            error => {
                this.alertService.error(error);
            });
    }
}