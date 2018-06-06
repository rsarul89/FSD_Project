import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/toPromise';

import { WorkoutCategory } from '../models/index';
import { AlertService } from '../services/alert.service';

@Injectable()
export class CategoryService {

    private categoryCollection: WorkoutCategory[];

    constructor(private httpClient: Http, private alertService: AlertService) {
        this.GetCategories()
            .then(categories => this.categoryCollection = categories);
    }

    GetCategories(): Promise<WorkoutCategory[]> {
        return this.httpClient.get("workout/getCategories")
            .toPromise()
            .then(response => response.json() as WorkoutCategory[])
            .catch(this.handleError);
    }

    GetCategory(id: number): Promise<WorkoutCategory> {
        let c = this.categoryCollection.find(c => c.category_id == id);
        let body = JSON.stringify(c);
        return this.httpClient.post("workout/getCategory", body)
            .toPromise()
            .then(response => response.json() as WorkoutCategory)
            .catch(this.handleError);
    }

    AddCategory(wc: WorkoutCategory): Promise<WorkoutCategory> {
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/addCategory", body)
            .toPromise()
            .then(response => response.json() as WorkoutCategory)
            .catch(this.handleError);
    }

    DeleteCategory(wc: WorkoutCategory): Promise<WorkoutCategory> {
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/deleteCategory", body)
            .toPromise()
            .then(response => response.json() as WorkoutCategory)
            .catch(this.handleError);
    }


    UpdateCategory(wc: WorkoutCategory): Promise<WorkoutCategory> {
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/updateCategory", body)
            .toPromise()
            .then(response => response.json() as WorkoutCategory)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        this.alertService.error(error.message, false);
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}