import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

import { User, Report } from '../models/index';
import { AppSettings } from '../shared/index';
import { AlertService } from '../services/alert.service'

@Injectable()
export class ReportService {

    constructor(private httpClient: Http, private appSettings: AppSettings, private alertService: AlertService) {
    }

    weeklyReport(wr: Report): Promise<Report> {
        let body = JSON.stringify(wr);
        return this.httpClient.post("report/getWeeklyReport", body)
            .toPromise()
            .then(response => response.json() as Report)
            .catch(this.handleError);
    }

    monthlyReport(wr: Report): Promise<Report> {
        let body = JSON.stringify(wr);
        return this.httpClient.post("report/getMonthlyReport", body)
            .toPromise()
            .then(response => response.json() as Report)
            .catch(this.handleError);
    }

    yearlyReport(wr: Report): Promise<Report> {
        let body = JSON.stringify(wr);
        return this.httpClient.post("report/getYearlyReport", body)
            .toPromise()
            .then(response => response.json() as Report)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        this.alertService.error(error.message, false);
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}