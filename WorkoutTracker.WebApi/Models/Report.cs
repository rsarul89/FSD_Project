using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class Report
    {
        public int IncrId { get; set; }
        public bool IsPrev { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Prev { get; set; }
        public int Next { get; set; }
        public User User { get; set; }
        public double[] Data { get; set; }
    }
}