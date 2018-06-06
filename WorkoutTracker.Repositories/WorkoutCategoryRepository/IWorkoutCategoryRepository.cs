using System.Collections.Generic;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories
{
    public interface IWorkoutCategoryRepository : IGenericRepository<workout_category>
    {
        void AddCategory(workout_category wc);
        void DeleteCategory(workout_category wc);
        workout_category UpdateCategory(workout_category wc);
        workout_category GetCategory(int categoryId);
        IEnumerable<workout_category> GetCategories();
    }
}
