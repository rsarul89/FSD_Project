import { WorkoutCollection } from './index';

export class WorkoutCategory {
    constructor(
        public category_id: number,
        public category_name: string,
        public workout_collection: Array<WorkoutCollection> 
    ) { }
}