using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Services
{
    public interface IWorkoutCategoryService : IEntityService<workout_category>
    {
        IEnumerable<workout_category> GetWorkoutCategories();
        void CreateWorkoutCategory(workout_category wc);
        workout_category UpdateWorkoutCategory(workout_category wc);
        void DeleteWorkoutCategory(workout_category wc);
        workout_category GetCategory(int categoryId);
    }
}
