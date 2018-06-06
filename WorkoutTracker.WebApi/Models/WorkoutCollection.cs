using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class WorkoutCollection
    {
        public WorkoutCollection()
        {
            this.workout_active = new List<WorkoutActive>();
        }

        public int workout_id { get; set; }
        public int category_id { get; set; }
        public string workout_title { get; set; }
        public string workout_note { get; set; }
        public double calories_burn_per_min { get; set; }
        public int user_id { get; set; }

        public User user { get; set; }
        public IList<WorkoutActive> workout_active { get; set; }
        public WorkoutCategory workout_category { get; set; }
    }
}