using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.Entities;
using WorkoutTracker.Services;
using WorkoutTracker.WebApi.Models;

namespace WorkoutTracker.WebApi.Controllers
{
    [RoutePrefix("api/report")]
    [Authentication]
    public class ReportController : BaseAPIController
    {
        private IWorkoutCollectionService _workoutService;
        private ILogManager _logManager;

        public ReportController(IWorkoutCollectionService workoutService, ILogManager logManager)
        {
            _workoutService = workoutService;
            _logManager = logManager;
        }

        [HttpPost]
        [Route("getWeeklyReport")]
        public HttpResponseMessage GetWeeklyReport(Report wr)
        {
            Report weeklyReport = new Report();
            try
            {
                DateTime firstOfWeek = new DateTime();
                if (wr.IsPrev)
                    firstOfWeek = DateTime.Today.AddDays(wr.IncrId * -7).FirstOfWeek();
                else
                    firstOfWeek = DateTime.Today.AddDays(wr.IncrId * 7).FirstOfWeek();
                DateTime lastofWeek = firstOfWeek.AddDays(7).AddSeconds(-1);
                var wDays = Convert.ToInt32(CalcNoOfDays(firstOfWeek, lastofWeek));
                var res = _workoutService.GetWorkoutsByUser(wr.User.user_name).ToList();
                if (res != null && res.Count > 0)
                {
                    weeklyReport.Data = new double[wDays];
                    res.ForEach(items =>
                    {
                        GenerateWeeklyReport(items, firstOfWeek, lastofWeek, ref weeklyReport);
                    });
                    weeklyReport.IncrId = wr.IncrId;
                }
                weeklyReport.FromDate = firstOfWeek.Day.ToString() + "/" + firstOfWeek.Month.ToString() + "/" + firstOfWeek.Year.ToString();
                weeklyReport.ToDate = lastofWeek.Day.ToString() + "/" + lastofWeek.Month.ToString() + "/" + lastofWeek.Year.ToString();
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(weeklyReport);
        }

        [HttpPost]
        [Route("getMonthlyReport")]
        public HttpResponseMessage GetMonthlyReport(Report mr)
        {
            Report monthyReport = new Report();
            try
            {
                DateTime firstOfWeek = new DateTime();
                if (mr.IsPrev)
                    firstOfWeek = DateTime.Today.AddMonths(mr.IncrId * -1).FirstOfWeek();
                else
                    firstOfWeek = DateTime.Today.AddMonths(mr.IncrId * 1).FirstOfWeek();

                DateTime lastofWeek = firstOfWeek.AddMonths(1).AddSeconds(-1);
                var weeks = CalcWeeks(firstOfWeek, lastofWeek);
                var res = _workoutService.GetWorkoutsByUser(mr.User.user_name).ToList();
                if (res != null && res.Count > 0)
                {
                    monthyReport.Data = new double[weeks.Count()];
                    res.ForEach(items =>
                    {
                        GenerateMonthlyReport(weeks,items, firstOfWeek, lastofWeek, ref monthyReport);
                    });
                    monthyReport.IncrId = mr.IncrId;
                }
                monthyReport.FromDate = firstOfWeek.Day.ToString() + "/" + firstOfWeek.Month.ToString() + "/" + firstOfWeek.Year.ToString();
                monthyReport.ToDate = lastofWeek.Day.ToString() + "/" + lastofWeek.Month.ToString() + "/" + lastofWeek.Year.ToString();
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(monthyReport);
        }

        [HttpPost]
        [Route("getYearlyReport")]
        public HttpResponseMessage GetYearlyReport(Report yr)
        {
            Report yearlyReport = new Report();
            try
            {
                DateTime firstOfWeek = new DateTime();
                if (yr.IsPrev)
                    firstOfWeek = DateTime.Today.AddMonths(yr.IncrId * -12).FirstDayOfMonth();
                else
                    firstOfWeek = DateTime.Today.AddMonths(yr.IncrId * 12).FirstDayOfMonth();

                DateTime lastofWeek = firstOfWeek.AddYears(1).AddSeconds(-1);
                var months = CalcMonths(firstOfWeek, lastofWeek);
                var noOfMonths = CalcNoOfMonths(firstOfWeek, lastofWeek);
                var res = _workoutService.GetWorkoutsByUser(yr.User.user_name).ToList();
                if (res != null && res.Count > 0)
                {
                    yearlyReport.Data = new double[noOfMonths];
                    res.ForEach(items =>
                    {
                        GenerateYearlyReport(months, items, firstOfWeek, lastofWeek, ref yearlyReport);
                    });
                    yearlyReport.IncrId = yr.IncrId;
                }
                yearlyReport.FromDate = firstOfWeek.Day.ToString() + "/" + firstOfWeek.Month.ToString() + "/" + firstOfWeek.Year.ToString();
                yearlyReport.ToDate = lastofWeek.Day.ToString() + "/" + lastofWeek.Month.ToString() + "/" + lastofWeek.Year.ToString();
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(yearlyReport);
        }
        private void GenerateYearlyReport(List<DateTime> wk, workout_collection wc, DateTime fow, DateTime low, ref Report rpt)
        {
            var wa = wc.workout_active.Where(w => w.start_date.Value >= fow && w.start_date.Value <= low && w.status == true).ToList();
            if (wa != null && wa.Count > 0)
            {
                for (int i = 0; i < wk.Count(); i++)
                {
                    var wek = wk.ToArray();
                    var data = wa.Where(x => x.start_date >= wek[i].FirstDayOfMonth() && x.start_date <= wek[i].LastDayOfMonth()).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int j = 0; j < data.Count(); j++)
                        {
                            GenerateYearlyData(data.Sum(item => item.workout_collection.calories_burn_per_min), i, ref rpt);
                        }
                    }
                }
            }
        }
        private void GenerateWeeklyReport(workout_collection wc, DateTime fow, DateTime low, ref Report rpt)
        {
            var wa = wc.workout_active.Where(w => w.start_date.Value >= fow && w.start_date.Value <= low && w.status == true).ToList();
            if (wa != null && wa.Count > 0)
            {
                for (int i = 0; i < wa.Count; i++)
                {
                    GenerateWeeklyData(wa[i].start_date.Value.DayOfWeek.ToString(), wa[i].workout_collection.calories_burn_per_min, ref rpt);
                }
            }
        }
        private void GenerateMonthlyReport(List<DateTime> wk, workout_collection wc, DateTime fow, DateTime low, ref Report rpt)
        {
            var wa = wc.workout_active.Where(w => w.start_date.Value >= fow && w.start_date.Value <= low && w.status == true).ToList();
            if (wa != null && wa.Count > 0)
            {
                for (int i = 0; i < wk.Count(); i++)
                {
                    var wek = wk.ToArray();
                    var data = wa.Where(x => x.start_date >= wek[i].FirstOfWeek() && x.start_date <= wek[i].AddDays(7).AddSeconds(-1)).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int j = 0; j < data.Count(); j++)
                        {
                            GenerateMonthlyData(data.Sum(item => item.workout_collection.calories_burn_per_min), i, ref rpt);
                        }
                    }
                }
            }
        }
        private void GenerateYearlyData(double value, int idx, ref Report rpt)
        {
            switch (idx)
            {
                case 0:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 1:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 2:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 3:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 4:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 5:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 6:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 7:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 8:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 9:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 10:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 11:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 12:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                default:
                    break;
            }
        }
        private void GenerateMonthlyData(double value, int idx, ref Report rpt)
        {
            switch (idx)
            {
                case 0:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 1:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 2:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 3:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                case 4:
                    rpt.Data[idx] = rpt.Data[idx] + value;
                    break;
                default:
                    break;
            }
        }
        private void GenerateWeeklyData(string wof, double value, ref Report rpt)
        {
            WeekDays wd;
            if (Enum.TryParse(wof, out wd))
            {
                switch (wd)
                {
                    case WeekDays.Monday:
                        rpt.Data[0] = rpt.Data[0] + value;
                        break;
                    case WeekDays.Tuesday:
                        rpt.Data[1] = rpt.Data[1] + value;
                        break;
                    case WeekDays.Wednesday:
                        rpt.Data[2] = rpt.Data[2] + value;
                        break;
                    case WeekDays.Thursday:
                        rpt.Data[3] = rpt.Data[3] + value;
                        break;
                    case WeekDays.Friday:
                        rpt.Data[4] = rpt.Data[4] + value;
                        break;
                    case WeekDays.Saturday:
                        rpt.Data[5] = rpt.Data[5] + value;
                        break;
                    case WeekDays.Sunday:
                        rpt.Data[6] = rpt.Data[6] + value;
                        break;
                    default:
                        break;
                }
            }
        }
        private List<DateTime> CalcWeeks(DateTime start, DateTime end)
        {
            return Enumerable.Range(0, (int)((end - start).TotalDays) + 1)
                  .Select(n => start.AddDays(n))
                  .Where(x => x.DayOfWeek == DayOfWeek.Monday)
                  .ToList();
        }
        private List<DateTime> CalcMonths(DateTime start, DateTime end)
        {
            List<DateTime> res = new List<DateTime>();
            for (int i = 1; i <= 12; i++)
            {
                var date = start.AddMonths(i * 1).AddSeconds(-1).FirstDayOfMonth();
                if (date >= start && date <= end)
                    res.Add(date);
            }
            return res;
        }
        private double CalcNoOfDays(DateTime start, DateTime end)
        {
            var res = Math.Abs(Math.Ceiling((start - end).TotalDays));
            return res;
        }
        private int CalcNoOfMonths(DateTime start, DateTime end)
        {
            var res = (end.Year - start.Year) * 12 + end.Month - start.Month;
            return res;
        }
    }
}
