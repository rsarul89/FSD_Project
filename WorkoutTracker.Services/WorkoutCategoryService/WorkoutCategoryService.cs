using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;

namespace WorkoutTracker.Services
{
    public class WorkoutCategoryService : EntityService<workout_category>, IWorkoutCategoryService
    {
        IUnitOfWork unitOfWork;
        IWorkoutCategoryRepository workoutCategoryRepository;

        public WorkoutCategoryService(IUnitOfWork unitOfWork, IWorkoutCategoryRepository workoutCategoryRepository) : base(unitOfWork, workoutCategoryRepository)
        {
            this.workoutCategoryRepository = workoutCategoryRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateWorkoutCategory(workout_category wc)
        {
            workoutCategoryRepository.AddCategory(wc);
            unitOfWork.Commit();
        }

        public void DeleteWorkoutCategory(workout_category wc)
        {
            workoutCategoryRepository.DeleteCategory(wc);
            unitOfWork.Commit();
        }

        public workout_category GetCategory(int categoryId)
        {
            return workoutCategoryRepository.GetCategory(categoryId);
        }

        public IEnumerable<workout_category> GetWorkoutCategories()
        {
            return workoutCategoryRepository.GetCategories();
        }

        public workout_category UpdateWorkoutCategory(workout_category wc)
        {
            var result  = workoutCategoryRepository.UpdateCategory(wc);
            unitOfWork.Commit();
            return result;
        }
    }
}
