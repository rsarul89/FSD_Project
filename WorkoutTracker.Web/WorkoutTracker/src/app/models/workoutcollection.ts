import { WorkoutCategory, WorkoutActive, User } from './index';

export class WorkoutCollection {
    constructor(
        public workout_id: number,
        public category_id: number,
        public workout_title: string,
        public workout_note: string,
        public calories_burn_per_min: number,
        public user_id: number,
        public user: User,
        public workout_category: WorkoutCategory,
        public workout_active: Array<WorkoutActive>
    ) { }
}