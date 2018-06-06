import { Component, OnInit, OnDestroy, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';


import { AlertService, WorkoutService } from '../../../services/index';
import { WorkoutCollection, WorkoutActive } from '../../../models/index';

@Component({
    moduleId: module.id,
    templateUrl: './workout.end.component.html',
    styleUrls: ['./workout.end.component.css']
})

export class WorkoutEndComponent implements OnInit, OnDestroy, AfterViewInit {

    public id: number;
    public edate: any;
    public etime: any;
    public comment: string;
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


        this.edate = today;
        this.etime = h1 + ':' + m1;
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
        this.wa.sid = this.model.workout_active[0].sid;
        this.wa.workout_id = this.model.workout_id;
        this.wa.start_date = this.model.workout_active[0].start_date;
        this.wa.start_time = this.model.workout_active[0].start_time;
        this.wa.end_date = this.edate;
        this.wa.end_time = this.etime;
        this.wa.comment = this.comment;
        this.wa.status = true;
        this.workoutService.EndWorkout(this.wa)
            .then(response => {
                this.alertService.success('Workout ended successfully', true);
                this.router.navigate(['/workout'], { relativeTo: this.route });
            });
    }
    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}