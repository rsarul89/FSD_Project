import { WorkoutCollection } from './index';

export class WorkoutActive {
    constructor(
        public sid: number,
        public workout_id: number,
        public start_time: any,
        public start_date: any,
        public end_date: any,
        public end_time: any,
        public comment: any,
        public status: boolean,
        public workout_collection: WorkoutCollection
    ) { }
}