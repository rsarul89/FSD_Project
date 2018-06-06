using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories
{
    public class WorkoutCategoryRepository : GenericRepository<workout_category>, IWorkoutCategoryRepository
    {
        public WorkoutCategoryRepository(DbContext context)
           : base(context) { }

        public void AddCategory(workout_category wc)
        {
            Add(wc);
        }

        public void DeleteCategory(workout_category wc)
        {
            var result = FindBy(w => w.category_id == wc.category_id).FirstOrDefault();
            Delete(result);
        }

        public IEnumerable<workout_category> GetCategories()
        {
            return GetAll();
        }

        public workout_category GetCategory(int categoryId)
        {
            return FindBy(w => w.category_id == categoryId)
                .AsQueryable()
                .Include(w => w.workout_collection)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public workout_category UpdateCategory(workout_category wc)
        {
            var entry = _entities.Entry<workout_category>(wc);
            if (entry.State == System.Data.EntityState.Detached)
            {
                var set = _entities.Set<workout_category>();
                var attachedEntity = set.Find(wc.category_id);
                if (attachedEntity != null)
                {
                    var attachedEntry = _entities.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(wc);
                }
                else
                {
                    entry.State = System.Data.EntityState.Modified;
                }
            }
            var result = FindBy(w => w.category_id == wc.category_id).FirstOrDefault();
            return result;
        }
    }
}
