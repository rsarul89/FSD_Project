import { WorkoutCollection } from './index';

export class User {
    constructor(
        public user_id: number,
        public user_name: string,
        public password: string,
        public workout_collection: Array<WorkoutCollection>
    ) { }
}