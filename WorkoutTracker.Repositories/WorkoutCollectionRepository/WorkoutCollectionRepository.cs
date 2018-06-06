using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories
{
    public class WorkoutCollectionRepository : GenericRepository<workout_collection>, IWorkoutCollectionRepository
    {
        public WorkoutCollectionRepository(DbContext context)
           : base(context) { }

        public void AddWorkout(workout_collection wc)
        {
            Add(wc);
        }

        public void DeleteWorkout(workout_collection wc)
        {
            var result = FindBy(w => w.workout_id == wc.workout_id).FirstOrDefault();
            Delete(result);
        }

        public IEnumerable<workout_active> GetActiveWorkouts(int workoutId)
        {
            var activeWorkouts = FindBy(w => w.workout_id == workoutId)
                .AsQueryable()
                .Include(x => x.workout_active)
                .FirstOrDefault().workout_active
                .AsEnumerable();
            return activeWorkouts;
        }

        public void StartWorkout(workout_active wa)
        {
            this._entities.Set<workout_active>().Add(wa);
        }

        public void EndWorkout(workout_active wa)
        {
            var entry = _entities.Entry<workout_active>(wa);
            if (entry.State == System.Data.EntityState.Detached)
            {
                var set = _entities.Set<workout_active>();
                var attachedEntity = set.Find(wa.sid);
                if (attachedEntity != null)
                {
                    var attachedEntry = _entities.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(wa);
                }
                else
                {
                    entry.State = System.Data.EntityState.Modified; 
                }
            }
            //this._entities.Entry(wa).State = System.Data.EntityState.Modified;
        }

        public workout_collection UpdateWorkout(workout_collection wc)
        {
            //Update(wc);
            var entry = _entities.Entry<workout_collection>(wc);
            if (entry.State == System.Data.EntityState.Detached)
            {
                var set = _entities.Set<workout_collection>();
                var attachedEntity = set.Find(wc.workout_id);
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
            var result = FindBy(w => w.workout_id == wc.workout_id).FirstOrDefault();
            return result;
        }

        public IEnumerable<workout_collection> GetAllWorkoutsByUser(string userName)
        {
            var wc = FindBy(w => w.user.user_name == userName)
                .AsQueryable()
                .Include(w => w.workout_active)
                .Include(w => w.workout_category)
                .Include(w => w.user)
                .AsEnumerable();
            return wc;
        }

        public workout_collection GetWorkout(int workoutId)
        {
            return FindBy(w => w.workout_id == workoutId)
                .AsQueryable()
                .Include(w => w.workout_active)
                .Include(w => w.workout_category)
                .Include(w => w.user)
                .AsEnumerable()
                .FirstOrDefault();
        }
    }
}
