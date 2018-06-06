import { Component, OnInit, OnDestroy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';


import { AlertService, WorkoutService } from '../../../services/index';
import { WorkoutCollection, WorkoutActive } from '../../../models/index';

@Component({
    moduleId: module.id,
    templateUrl: './workout.start.component.html',
    styleUrls: ['./workout.start.component.css']
})

export class WorkoutStartComponent implements OnInit, OnDestroy, AfterViewInit {

    public id: number;
    public sdate: any;
    public stime: any;
    public sub: Subscription;
    public model: WorkoutCollection;
    public wa: WorkoutActive;

    constructor(private alertService: AlertService,
        private workoutService: WorkoutService,
        private route: ActivatedRoute,
        private router: Router,
        private cdr: ChangeDetectorRef) {
    }

    ngAfterViewInit() {
        let date = new Date();
        let day = date.getDate();
        let month = date.getMonth() + 1;
        let month1: string;
        let day1: string;
        let year = date.getFullYear();
        let h = date.getHours();
        let h1: string;
        let m = date.getMinutes();
        let m1: string;

        if (h < 10) {
            h1 = '0' + h;
        }
        else {
            h1 = h.toString();
        }
        if (m < 10) {
            m1 = '0' + m;
        }
        else {
            m1 = m.toString();
        }

        if (month < 10) {
            month1 = "0" + month;
        }
        else {
            month1 = month.toString();
        }
        if (day < 10) {
            day1 = "0" + day;
        }
        else {
            day1 = day.toString();
        }

        let today = year + "-" + month1 + "-" + day1;

        this.sdate = today;
        this.stime = h1 + ':' + m1;
        this.cdr.detectChanges();
    }

    ngOnInit() {

        this.model = new WorkoutCollection(0, 0, "", "", 0, 0, null, null, null);
        this.wa = new WorkoutActive(0,0, null, null, null, null, "", false, null);
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['workout-id'];
        });

        this.getWorkout();
    }

    getWorkout() {
        this.workoutService.GetActiveWorkout(this.id)
            .then(workout => {
                this.model = workout;
            });
    }

    onFormSubmit() {
        this.wa.workout_collection = this.model;
        this.wa.workout_id = this.model.workout_id;
        this.wa.start_date = this.sdate;
        this.wa.start_time = this.stime;
        this.wa.status = false;
        this.workoutService.StartWorkout(this.wa)
            .then(response => {
                this.alertService.success('Workout started successfully', true);
                this.router.navigate(['/workout'], { relativeTo: this.route });
            });
    }
    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}