import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import 'rxjs/add/operator/switchMap';


import { AlertService, WorkoutService, CategoryService } from '../../../services/index';
import { WorkoutCollection, WorkoutCategory } from '../../../models/index';
import { AppSettings } from '../../../shared/index';

@Component({
    moduleId: module.id,
    templateUrl: './workout.edit.component.html',
    styleUrls: ['./workout.edit.component.css']
})

export class WorkoutEditComponent implements OnInit, OnDestroy {

    public id: number;
    public workoutCategory: WorkoutCategory[];
    public categories: WorkoutCategory[];
    public sub: Subscription;
    public model: WorkoutCollection;

    constructor(private alertService: AlertService,
        private workoutService: WorkoutService,
        private categoryService: CategoryService,
        private appSettings: AppSettings,
        private route: ActivatedRoute,
        private router: Router) {
    }

    ngOnInit() {

        this.model = new WorkoutCollection(0, 0, "", "", 0, 0, null, null, null);
        this.categoryService.GetCategories()
            .then(response => {
                this.categories = response;
            });
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['workout-id'];
        });

        this.getWorkout();
    }

    getWorkout() {
        this.workoutService.GetWorkout(this.id)
            .then(workout => {
                this.model = workout;
            });
    }

    onFormSubmit() {
        this.workoutService.UpdateWorkout(this.model)
            .then(response => {
                this.alertService.success('Workout updated successfully', true);
                this.router.navigate(['/workout'], { relativeTo: this.route });
            });
    }
    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}