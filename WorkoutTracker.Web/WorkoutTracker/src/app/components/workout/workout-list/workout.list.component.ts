import { Component, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router'; 
import { WorkoutCollection } from '../../../models/index';
import { AlertService, WorkoutService } from '../../../services/index';

@Component({
    moduleId: module.id,
    selector: 'workout-list',
    templateUrl: './workout.list.component.html',
    styleUrls: ['./workout.list.component.css']
})
export class WorkoutListComponent implements OnInit {

    @Input() workout: WorkoutCollection;
    @Output() deleteWorkout: EventEmitter<WorkoutCollection> = new EventEmitter<WorkoutCollection>();
    @Output() startWorkout: EventEmitter<WorkoutCollection> = new EventEmitter<WorkoutCollection>();
    @Output() endWorkout: EventEmitter<WorkoutCollection> = new EventEmitter<WorkoutCollection>();

    constructor(private alertService: AlertService, private router: Router,
        private activatedRoute: ActivatedRoute,
        private workoutService: WorkoutService) { }

    ngOnInit() {
    }

    DeleteWorkout(wc: WorkoutCollection) {
        this.workoutService.DeleteWorkout(wc)
            .then(
            response => {
                if (response != null) {
                    this.deleteWorkout.emit(this.workout);
                    this.alertService.success('Workout deleted successfully', false);
                }
                else {
                    this.alertService.error('Problem on deleting workout, please try again.', true);
                }
            }).
            catch(
            error => {
                this.alertService.error(error);
            });
    }

    Edit(wc: WorkoutCollection) {
        this.router.navigate(['/workout/edit', wc.workout_id], { relativeTo: this.activatedRoute }); 
    }

    StartWorkout(wc: WorkoutCollection) {
        this.router.navigate(['/workout/start', wc.workout_id], { relativeTo: this.activatedRoute });
    }

    EndWorkout(wc: WorkoutCollection) {
        this.router.navigate(['/workout/end', wc.workout_id], { relativeTo: this.activatedRoute });
    }

    StartButtonEnableDisable(wc: WorkoutCollection) {
        let wa = wc.workout_active.find(w => w.workout_id == wc.workout_id);
        let res = false;
        if (wa === undefined)
            res = false;
        else if (wa !== undefined && wa.status == false && (wa.end_date == null && wa.end_time == null))
            res = true;
        else if (wa !== undefined && wa.status == true)
            res = true;
        return res;
    }

    EndButtonEnableDisable(wc: WorkoutCollection) {
        let wa = wc.workout_active.find(w => w.workout_id == wc.workout_id);
        let res = false;
        if (wa === undefined)
            res = true;
        else if (wa !== undefined && wa.status == false && (wa.start_date !== null && wa.start_time !== null))
            res = false;
        else if (wa !== undefined && wa.status == true)
            res = true;
        return res;
    }
}
