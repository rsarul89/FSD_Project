using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class WorkoutActive
    {
        public int sid { get; set; }
        public int workout_id { get; set; }
        public Nullable<System.TimeSpan> start_time { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public Nullable<System.TimeSpan> end_time { get; set; }
        public string comment { get; set; }
        public Nullable<bool> status { get; set; }

        public WorkoutCollection workout_collection { get; set; }
    }
}