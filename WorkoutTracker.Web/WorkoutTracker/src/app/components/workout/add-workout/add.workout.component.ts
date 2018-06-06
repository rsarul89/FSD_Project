import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { WorkoutCollection, WorkoutCategory, LoginResponse } from '../../../models/index';
import { AlertService, WorkoutService, CategoryService } from '../../../services/index';
import { AppSettings } from '../../../shared/index';

@Component({
    moduleId: module.id,
    templateUrl: './add.workout.component.html',
    styleUrls: ['./add.workout.component.css']
})
export class AddWorkoutComponent implements OnInit {
    constructor(
        private alertService: AlertService,
        private workoutService: WorkoutService,
        private categoryService: CategoryService,
        private appSettings: AppSettings,
        private route: ActivatedRoute,
        private router: Router) { }

    public min: number = 0;
    public step: number = 0.1;
    public precision: number = 1;
    public max: number = 1000;
    public disabled: boolean = true;
    public categories: WorkoutCategory[];
    model = new WorkoutCollection(0, 0, "", "", this.min, 0, null, null, null);

    ngOnInit() {
        this.categoryService.GetCategories()
            .then(response => {
                this.categories = response;
            });
    }

    onFormSubmit() {
        let workout_title: string = this.model.workout_title;
        let workout_note: string = this.model.workout_note;
        let calories_burn_per_min: number = this.model.calories_burn_per_min;
        let workout_category: number = this.model.category_id;

        let user: LoginResponse = this.appSettings.getLoggedUser();

        let workout = new WorkoutCollection(null, workout_category, workout_title, workout_note, calories_burn_per_min, user.UserID, null, null, null);
        this.workoutService.AddWorkout(workout)
            .then(data => {
                this.router.navigate(['../workout'], { relativeTo: this.route })
            });
    }

    public increaseValue(workoutForm: NgForm): void {
        var currentValue = workoutForm.controls["calories"].value;
        if (currentValue < this.max) {
            currentValue = currentValue + this.step;
            if (this.precision != null) {
                currentValue = this.round(currentValue, this.precision);
            }
            workoutForm.controls["calories"].setValue(currentValue);
        }
    }

    public decreaseValue(workoutForm: NgForm): void {
        var currentValue = workoutForm.controls["calories"].value;
        if (currentValue > this.min) {
            currentValue = currentValue - this.step;
            if (this.precision != null) {
                currentValue = this.round(currentValue, this.precision);
            }
            workoutForm.controls["calories"].setValue(currentValue);
        }
    }

    public round(value: number, precision: number): number {
        let multiplier: number = Math.pow(10, precision || 0);
        return Math.round(value * multiplier) / multiplier;
    }

    public resetWorkoutForm(workoutForm: NgForm) {
        workoutForm.resetForm({
            title: '',
            note: '',
            calories: 0,
            category: 0
        });
    }

    public validate(workoutForm: NgForm): boolean {
        let res = false;
        if (Object.keys(workoutForm.controls).length > 0) {
            if (workoutForm.controls["title"].invalid)
                res = true;
            else if (workoutForm.controls["calories"].value <= 0)
                res = true;
            else if (workoutForm.controls["category"].value <= 0)
                res = true;
        }
        return res;
    }
}
