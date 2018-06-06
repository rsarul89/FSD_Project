using System.Collections.Generic;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories
{
    public interface IWorkoutCollectionRepository : IGenericRepository<workout_collection>
    {
        IEnumerable<workout_active> GetActiveWorkouts(int workoutId);
        workout_collection GetWorkout(int workoutId);
        IEnumerable<workout_collection> GetAllWorkoutsByUser(string userName);
        void StartWorkout(workout_active wa);
        void EndWorkout(workout_active wa);
        void AddWorkout(workout_collection wc);
        void DeleteWorkout(workout_collection wc);
        workout_collection UpdateWorkout(workout_collection wc);
    }
}
