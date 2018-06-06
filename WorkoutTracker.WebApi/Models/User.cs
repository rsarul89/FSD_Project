using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class User
    {
        public User()
        {
            this.workout_collection = new List<WorkoutCollection>();
        }

        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }

        public IList<WorkoutCollection> workout_collection { get; set; }
    }
}