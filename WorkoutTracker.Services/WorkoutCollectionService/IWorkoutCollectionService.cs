using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Services
{
    public interface IWorkoutCollectionService : IEntityService<workout_collection>
    {
        IEnumerable<workout_collection> GetWorkouts();
        IEnumerable<workout_collection> GetWorkoutsByUser(string username);
        IEnumerable<workout_active> GetActiveWorkouts(int workoutId);
        workout_collection GetWorkout(int workoutId);
        void CreateWorkout(workout_collection wc);
        void StartWorkout(workout_active wa);
        void EndWorkout(workout_active wa);
        workout_collection UpdateWorkout(workout_collection wc);
        void DeleteWorkout(workout_collection wc);
    }
}
