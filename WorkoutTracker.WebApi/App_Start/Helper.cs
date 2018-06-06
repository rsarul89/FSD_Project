using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi
{
    public static class Helper
    {
        public static T CastObject<T>(object source)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public static DateTime FirstOfWeek(this DateTime date)
        {
            return date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
        }
        // Extension method to return the first working day of the week (Monday).
        public static DateTime FirstOfWorkingWeek(this DateTime date)
        {
            return date.AddDays(DayOfWeek.Monday - date.DayOfWeek);
        }
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }
        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return value.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }
    }

    public enum WeekDays { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday }
}