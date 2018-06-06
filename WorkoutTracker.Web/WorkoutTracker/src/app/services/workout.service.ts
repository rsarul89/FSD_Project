import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { Register, WorkoutActive, WorkoutCollection, User } from '../models/index';
import { AlertService } from '../services/alert.service';
import { AppSettings } from '../shared/index';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class WorkoutService {

    private workoutCollection: WorkoutCollection[];

    constructor(private httpClient: Http, private alertService: AlertService, private appSettings: AppSettings) {
        let user = this.appSettings.getLoggedUser();
        let uname = user.UserName;
        this.GetWorkoutsByUser(uname)
            .then(workouts => this.workoutCollection = workouts);
    }

    GetWorkoutsByUser(user_name: string): Promise<WorkoutCollection[]> {
        let wc = new User(0, user_name, "", []);
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/getWorkoutsByUser", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection[])
            .catch(this.handleError);
    }

    GetWorkout(id: number): Promise<WorkoutCollection> {
        let wc = this.workoutCollection.find(wc => wc.workout_id == id);
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/getWorkout", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection)
            .catch(this.handleError);
    }

    GetActiveWorkout(id: number): Promise<WorkoutCollection> {
        let wc = this.workoutCollection.find(wc => wc.workout_id == id);
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/getWorkout", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection)
            .catch(this.handleError);
    }

    AddWorkout(wc: WorkoutCollection): Promise<WorkoutCollection> {
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/addWorkout", body)
            .toPromise()
            .then(response => { this.workoutCollection.push(response.json() as WorkoutCollection);response.json() as WorkoutCollection; })
            .catch(this.handleError);
    }

    DeleteWorkout(wc: WorkoutCollection): Promise<WorkoutCollection> {
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/deleteWorkout", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection)
            .catch(this.handleError);
    }

    StartWorkout(wa: WorkoutActive): Promise<WorkoutCollection> {
        let body = JSON.stringify(wa);
        return this.httpClient.post("workout/startWorkout", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection)
            .catch(this.handleError);
    }

    EndWorkout(wa: WorkoutActive): Promise<WorkoutCollection> {
        let body = JSON.stringify(wa);
        return this.httpClient.post("workout/endWorkout", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection)
            .catch(this.handleError);
    }

    UpdateWorkout(wc: WorkoutCollection): Promise<WorkoutCollection> {
        let body = JSON.stringify(wc);
        return this.httpClient.post("workout/updateWorkout", body)
            .toPromise()
            .then(response => response.json() as WorkoutCollection)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        this.alertService.error(error.message, false);
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}