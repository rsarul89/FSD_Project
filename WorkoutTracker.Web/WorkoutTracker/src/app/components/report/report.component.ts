import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Chart } from 'chart.js';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertService, ReportService } from '../../services/index';
import { Report, User } from '../../models/index';
import { AppSettings } from '../../shared/index';

@Component({
    moduleId: module.id,
    templateUrl: './report.component.html',
    styleUrls: ['./report.component.css']
})

export class ReportComponent implements OnInit {

    public weeklyChart: Chart;
    public monthlyChart: Chart;
    public yearlyChart: Chart;

    // Weekly
    weeklyDays = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    weeklyDaysData = [];
    weeklyChartCanvasDaysColor = ['#005999', '#ffff00', '#07c', '#f3955e', '#008000', '#e4e6e8', '#15157f'];
    weeklyReportCanvas: any;
    weeklyReportCtx: any;
    weeklyReportIncr: number = 0;
    weeklyPrev: number = 0;
    weeklyNext: number = 0;

    // Monthly
    monthlyWeeks = ['Week 1', 'Week 2', 'Week 3', 'Week 4', 'Week 5'];
    monthlyWeeksData = [];
    monthlyChartCanvasDaysColor = ['#005999', '#ffff00', '#07c', '#f3955e', '#008000'];
    monthlyReportCanvas: any;
    monthlyReportCtx: any;
    monthlyReportIncr: number = 0;
    monthlyPrev: number = 0;
    monthlyNext: number = 0;

    // Yearly
    yearlyWeeks = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'];
    yearlyWeeksData = [];
    yearlyChartCanvasDaysColor = ['#005999', '#ffff00', '#07c', '#f3955e', '#008000', '#e4e6e8', '#15157f', '#a276b5', '#64b7b7', '#386f0e', '#6a737c','#ab8c5d'];
    yearlyReportCanvas: any;
    yearlyReportCtx: any;
    yearlyReportIncr: number = 0;
    yearlyPrev: number = 0;
    yearlyNext: number = 0;


    IsPrev: boolean = false;
    weeklyReport: Report;
    monthlyReport: Report;
    yearlyReport: Report;
    user: User;
    firstLoad: boolean = true;

    constructor(private alertService: AlertService,
        private reportService: ReportService,
        private appSettings: AppSettings,
        private route: ActivatedRoute,
        private router: Router,
        private detector: ChangeDetectorRef) { }

    ngOnInit() {
        if (this.firstLoad) {
            this.GetWeeklyReportByUser();
            this.GetMonthlyReportByUser();
            this.GetYearlyReportByUser();
            this.firstLoad = false;
        }
        this.detector.detectChanges();
    }

    Prev(reportId: string) {
        this.IsPrev = true;
        if (reportId == 'weekly') {
            this.weeklyPrev++;
            this.weeklyNext--;
            this.weeklyReportIncr = this.weeklyPrev;
            this.GetWeeklyReportByUser();
        }
        if (reportId == 'monthly') {
            this.monthlyPrev++;
            this.monthlyNext--;
            this.monthlyReportIncr = this.monthlyPrev;
            this.GetMonthlyReportByUser();
        }
        if (reportId == 'yearly') {
            this.yearlyPrev++;
            this.yearlyNext--;
            this.yearlyReportIncr = this.yearlyPrev;
            this.GetYearlyReportByUser();
        }
    }

    Next(reportId: string) {
        this.IsPrev = false;
        if (reportId == 'weekly') {
            this.weeklyNext++;
            this.weeklyPrev--;
            this.weeklyReportIncr = this.weeklyNext;
            this.GetWeeklyReportByUser();
        }
        if (reportId == 'monthly') {
            this.monthlyNext++;
            this.monthlyPrev--;
            this.monthlyReportIncr = this.monthlyNext;
            this.GetMonthlyReportByUser();
        }
        if (reportId == 'yearly') {
            this.yearlyNext++;
            this.yearlyPrev--;
            this.yearlyReportIncr = this.yearlyNext;
            this.GetYearlyReportByUser();
        }
    }

    GetWeeklyReportByUser() {
        let uname = this.appSettings.getLoggedUser().UserName;
        if (uname) {
            this.user = new User(0, uname, "", null);
            this.weeklyReport = new Report(this.weeklyReportIncr, this.weeklyPrev, this.weeklyNext, this.IsPrev, "", "", null, this.user);
            this.reportService.weeklyReport(this.weeklyReport)
                .then(data => {
                    this.weeklyReport = data;
                    this.BuildWeeklyData(this.weeklyReport.Data);
                })
                .catch(error => { this.alertService.error(error) });
        }
        else {
            this.alertService.error("Problem on fetching weekly report, you will be redirected to login page", false);
            this.router.navigate(['login']);
        }
    }

    GetMonthlyReportByUser() {
        let uname = this.appSettings.getLoggedUser().UserName;
        if (uname) {
            this.user = new User(0, uname, "", null);
            this.monthlyReport = new Report(this.monthlyReportIncr, this.monthlyPrev, this.monthlyNext, this.IsPrev, "", "", null, this.user);
            this.reportService.monthlyReport(this.monthlyReport)
                .then(data => {
                    this.monthlyReport = data;
                    this.BuildMonthlyData(this.monthlyReport.Data);
                })
                .catch(error => { this.alertService.error(error) });
        }
        else {
            this.alertService.error("Problem on fetching monthly report, you will be redirected to login page", false);
            this.router.navigate(['login']);
        }
    }

    GetYearlyReportByUser() {
        let uname = this.appSettings.getLoggedUser().UserName;
        if (uname) {
            this.user = new User(0, uname, "", null);
            this.yearlyReport = new Report(this.yearlyReportIncr, this.yearlyPrev, this.yearlyNext, this.IsPrev, "", "", null, this.user);
            this.reportService.yearlyReport(this.yearlyReport)
                .then(data => {
                    this.yearlyReport = data;
                    this.BuildYearlyData(this.yearlyReport.Data);
                })
                .catch(error => { this.alertService.error(error) });
        }
        else {
            this.alertService.error("Problem on fetching yearly report, you will be redirected to login page", false);
            this.router.navigate(['login']);
        }
    }

    BuildWeeklyData(data: number[]) {
        this.weeklyDaysData = [];
        if (data != null) {
            data.forEach((i) => {
                this.weeklyDaysData.push(i);
            });
            this.BuildWeeklyReport();
        }
    }

    BuildMonthlyData(data: number[]) {
        this.monthlyWeeksData = [];
        if (data != null) {
            data.forEach((i) => {
                this.monthlyWeeksData.push(i);
            });
            this.BuildMonthlyReport();
        }
    }

    BuildYearlyData(data: number[]) {
        this.yearlyWeeksData = [];
        if (data != null) {
            data.forEach((i) => {
                this.yearlyWeeksData.push(i);
            });
            this.BuildYearlyReport();
        }
    }

    BuildWeeklyReport(): void {
        if (this.weeklyChart) this.weeklyChart.destroy();
        this.weeklyReportCanvas = document.getElementById('weeklyChartCanvas');
        this.weeklyReportCtx = this.weeklyReportCanvas.getContext('2d');
        this.weeklyChart = new Chart(this.weeklyReportCtx, {
            type: 'bar',
            data: {
                labels: this.weeklyDays,
                datasets: [
                    {
                        data: this.weeklyDaysData,
                        backgroundColor: this.weeklyChartCanvasDaysColor,
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                tooltips: {
                    enabled: true,
                    mode: 'single',
                    callbacks: {
                        label: function (tooltipItems, data) {
                            return tooltipItems.yLabel + ' calories';
                        }
                    }
                },
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: 'Weekly Report'
                },
                scales: {
                    xAxes: [{
                        gridLines: { display: false },
                        display: true,
                        scaleLabel: { display: false, labelString: '' }
                    }],
                    yAxes: [{
                        gridLines: { display: false },
                        display: true,
                        scaleLabel: { display: false, labelString: '' }, ticks: { stepSize: 50, beginAtZero: true }
                    }],
                }
            }
        });
    }

    BuildMonthlyReport(): void {
        if (this.monthlyChart) this.monthlyChart.destroy();
        this.monthlyReportCanvas = document.getElementById('monthlyChartCanvas');
        this.monthlyReportCtx = this.monthlyReportCanvas.getContext('2d');
        this.monthlyChart = new Chart(this.monthlyReportCtx, {
            type: 'bar',
            data: {
                labels: this.monthlyWeeks,
                datasets: [
                    {
                        data: this.monthlyWeeksData,
                        backgroundColor: this.monthlyChartCanvasDaysColor,
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                tooltips: {
                    enabled: true,
                    mode: 'single',
                    callbacks: {
                        label: function (tooltipItems, data) {
                            return tooltipItems.yLabel + ' calories';
                        }
                    }
                },
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: 'Monthly Report'
                },
                scales: {
                    xAxes: [{
                        gridLines: { display: false },
                        display: true,
                        scaleLabel: { display: false, labelString: '' }
                    }],
                    yAxes: [{
                        gridLines: { display: false },
                        display: true,
                        scaleLabel: { display: false, labelString: '' }, ticks: { stepSize: 50, beginAtZero: true }
                    }],
                }
            }
        });
    }

    BuildYearlyReport(): void {
        if (this.yearlyChart) this.yearlyChart.destroy();
        this.yearlyReportCanvas = document.getElementById('yearlyChartCanvas');
        this.yearlyReportCtx = this.yearlyReportCanvas.getContext('2d');
        this.yearlyChart = new Chart(this.yearlyReportCtx, {
            type: 'bar',
            data: {
                labels: this.yearlyWeeks,
                datasets: [
                    {
                        data: this.yearlyWeeksData,
                        backgroundColor: this.yearlyChartCanvasDaysColor,
                        borderColor: "#3cba9f",
                        fill: false
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                tooltips: {
                    enabled: true,
                    mode: 'single',
                    callbacks: {
                        label: function (tooltipItems, data) {
                            return tooltipItems.yLabel + ' calories';
                        }
                    }
                },
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: 'Yearly Report'
                },
                scales: {
                    xAxes: [{
                        gridLines: { display: false },
                        display: true,
                        scaleLabel: { display: false, labelString: '' }
                    }],
                    yAxes: [{
                        gridLines: { display: false },
                        display: true,
                        scaleLabel: { display: false, labelString: '' }, ticks: { stepSize: 50, beginAtZero: true }
                    }],
                }
            }
        });
    }
}