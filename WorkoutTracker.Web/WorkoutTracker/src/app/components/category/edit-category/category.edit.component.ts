import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import 'rxjs/add/operator/switchMap';


import { AlertService, CategoryService } from '../../../services/index';
import { WorkoutCategory } from '../../../models/index';
import { AppSettings } from '../../../shared/index';

@Component({
    moduleId: module.id,
    templateUrl: './category.edit.component.html',
    styleUrls: ['./category.edit.component.css']
})

export class CategoryEditComponent implements OnInit, OnDestroy {

    public id: number;
    private sub: Subscription;
    public model: WorkoutCategory;

    constructor(private alertService: AlertService,
        private categoryService: CategoryService,
        private appSettings: AppSettings,
        private route: ActivatedRoute,
        private router: Router) {
    }

    ngOnInit() {

        this.model = new WorkoutCategory(0, "", null);
        this.sub = this.route.params.subscribe(params => {
            this.id = +params['category-id'];
        });

        this.getWorkout();
    }

    getWorkout() {
        this.categoryService.GetCategory(this.id)
            .then(category => {
                this.model = category;
            });
    }

    onFormSubmit() {
        this.categoryService.UpdateCategory(this.model)
            .then(response => {
                this.alertService.success('Workout category updated successfully', true);
                this.router.navigate(['/category'], { relativeTo: this.route });
            });
    }
    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }
}