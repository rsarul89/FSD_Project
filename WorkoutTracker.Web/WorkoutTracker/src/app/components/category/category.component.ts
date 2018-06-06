import { Component, OnInit } from '@angular/core';
import { AlertService, CategoryService } from '../../services/index';
import { WorkoutCategory } from '../../models/index';

@Component({
    moduleId: module.id,
    selector: 'category',
    templateUrl: './category.component.html',
    styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

    public workoutCategory: WorkoutCategory[];
    public model: WorkoutCategory;

    constructor(private alertService: AlertService,
        private categoryService: CategoryService) { }

    ngOnInit() {
        this.GetAllCategories();
        this.model = new WorkoutCategory(0, "", null);
    }

    GetAllCategories() {
        this.categoryService.GetCategories()
            .then(response => {
                this.workoutCategory = response;
            }).
            catch(
            error => {
                this.alertService.error("Problem on fetching categories", false);
            });
    }

    hasData(): boolean {
        return (this.workoutCategory != null && this.workoutCategory.length > 0);
    }

    onDeleteCategory(item: WorkoutCategory) {
        this.workoutCategory.splice(this.workoutCategory.indexOf(item), 1);
    }

    addCategory(): void {
        let wc = this.model;
        this.categoryService.AddCategory(wc)
            .then(response => {
                this.workoutCategory.push(response);
            }).catch(
            error => {
                this.alertService.error("Problem on add category", false);
            });
    }
}
