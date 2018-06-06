import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertService, WorkoutService } from '../../services/index';
import { WorkoutCollection } from '../../models/index';
import { AppSettings } from '../../shared/index';

@Component({
  moduleId: module.id,
  selector: 'workout',
  templateUrl: './workout.component.html',
  styleUrls: ['./workout.component.css']
})

export class WorkoutComponent implements OnInit { 

    public workoutCollection: WorkoutCollection[];

    constructor(private alertService: AlertService,
        private workoutService: WorkoutService,
        private appSettings: AppSettings,
        private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {
        this.GetAllWorkoutsByUser();
    }

    GetAllWorkoutsByUser() {
        let uname = this.appSettings.getLoggedUser().UserName;
        if (uname) {
            this.workoutService.GetWorkoutsByUser(uname)
                .then(data => { this.workoutCollection = data; })
                .catch(error => {this.alertService.error(error)});
        }
        else {
            this.alertService.error("Problem on fetching workouts, you will be redirected to login page", false);
            this.router.navigate(['login']);
        }
    }

    onDeleteWorkout(item: WorkoutCollection) {
        this.workoutCollection.splice(this.workoutCollection.indexOf(item), 1);
    }

    onStartWorkout(item: WorkoutCollection) {
        let itemIndex = this.workoutCollection.findIndex(i => i.workout_id == item.workout_id);
        this.workoutCollection.splice(itemIndex, 1);
        this.workoutCollection.push(item);
    }

    onEndWorkout(item: WorkoutCollection) {
        let itemIndex = this.workoutCollection.findIndex(i => i.workout_id == item.workout_id);
        this.workoutCollection.splice(itemIndex, 1);
        this.workoutCollection.push(item);
    }

    hasData(): boolean {
        return (this.workoutCollection != null && this.workoutCollection.length > 0);
    }
}
    