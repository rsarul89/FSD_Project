using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class WorkoutCategory
    {
        public WorkoutCategory()
        {
            this.workout_collection = new List<WorkoutCollection>();
        }

        public int category_id { get; set; }
        public string category_name { get; set; }

        public IList<WorkoutCollection> workout_collection { get; set; }
    }
}