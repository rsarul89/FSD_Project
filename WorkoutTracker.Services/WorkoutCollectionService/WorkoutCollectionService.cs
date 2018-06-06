using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;

namespace WorkoutTracker.Services
{
    public class WorkoutCollectionService : EntityService<workout_collection>, IWorkoutCollectionService
    {
        IUnitOfWork unitOfWork;
        IWorkoutCollectionRepository workoutCollectionRepository;

        public WorkoutCollectionService(IUnitOfWork unitOfWork, IWorkoutCollectionRepository workoutCollectionRepository) : base(unitOfWork, workoutCollectionRepository)
        {
            this.workoutCollectionRepository = workoutCollectionRepository;
            this.unitOfWork = unitOfWork;
        }

        public void StartWorkout(workout_active wa)
        {
            workoutCollectionRepository.StartWorkout(wa);
            unitOfWork.Commit();
        }

        public void EndWorkout(workout_active wa)
        {
            workoutCollectionRepository.EndWorkout(wa);
            unitOfWork.Commit();
        }

        public void CreateWorkout(workout_collection wc)
        {
            workoutCollectionRepository.AddWorkout(wc);
            unitOfWork.Commit();
        }

        public void DeleteWorkout(workout_collection wc)
        {
            workoutCollectionRepository.DeleteWorkout(wc);
            unitOfWork.Commit();
        }

        public IEnumerable<workout_active> GetActiveWorkouts(int workoutId)
        {
            return workoutCollectionRepository.GetActiveWorkouts(workoutId);
        }

        public IEnumerable<workout_collection> GetWorkouts()
        {
            return workoutCollectionRepository.GetAll();
        }

        public IEnumerable<workout_collection> GetWorkoutsByUser(string username)
        {
            return workoutCollectionRepository.GetAllWorkoutsByUser(username);
        }

        public workout_collection UpdateWorkout(workout_collection wc)
        {
            var res = workoutCollectionRepository.UpdateWorkout(wc);
            unitOfWork.Commit();
            return res;
        }

        public workout_collection GetWorkout(int workoutId)
        {
            return workoutCollectionRepository.GetWorkout(workoutId);
        }
    }
}
