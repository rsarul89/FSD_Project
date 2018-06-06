import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { WorkoutCategory } from '../../../models/index';
import { AlertService, CategoryService } from '../../../services/index';

@Component({
    moduleId: module.id,
    selector: 'category-list',
    templateUrl: './category.list.component.html',
    styleUrls: ['./category.list.component.css']
})
export class CategoryListComponent implements OnInit {

    @Input() category: WorkoutCategory;
    @Output() deleteCategory: EventEmitter<WorkoutCategory> = new EventEmitter<WorkoutCategory>();

    constructor(private alertService: AlertService,
        private categoryService: CategoryService) { }

    ngOnInit() {

    }

    DeleteCategory(wc: WorkoutCategory) {
        this.categoryService.DeleteCategory(wc)
            .then(
            response => {
                if (response != null) {
                    this.deleteCategory.emit(this.category);
                    this.alertService.success('Category deleted successfully', false);
                }
                else {
                    this.alertService.error('Problem on deleting category, please try again.', false);
                }
            }).
            catch(
            error => {
                this.alertService.error(error);
            });
    }
}
