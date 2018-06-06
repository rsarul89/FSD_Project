using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.Entities;
using WorkoutTracker.Services;
using WorkoutTracker.WebApi.Models;

namespace WorkoutTracker.WebApi.Controllers
{
    [RoutePrefix("api/workout")]
    [Authentication]
    public class WorkoutTrackerController : BaseAPIController
    {
        private IWorkoutCollectionService _workoutService;
        private IWorkoutCategoryService _workoutCategoryService;
        private ILogManager _logManager;
        public WorkoutTrackerController(IWorkoutCollectionService workoutService, IWorkoutCategoryService workoutCategoryService, ILogManager logManager)
        {
            _workoutService = workoutService;
            _workoutCategoryService = workoutCategoryService;
            _logManager = logManager;
        }

        [HttpGet]
        [Route("getCategories")]
        public HttpResponseMessage GetCategories()
        {
            IEnumerable<WorkoutCategory> result = null;
            try
            {
                var res  = _workoutCategoryService.GetWorkoutCategories();
                result = Helper.CastObject<IEnumerable<WorkoutCategory>>(res);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("getCategory")]
        public HttpResponseMessage GetCategory(WorkoutCategory wc)
        {
            WorkoutCategory result = new WorkoutCategory();
            try
            {
                var res = _workoutCategoryService.GetCategory(wc.category_id);
                MapCategory(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("addCategory")]
        public HttpResponseMessage AddCategory(WorkoutCategory wc)
        {
            WorkoutCategory result = new WorkoutCategory();
            try
            {
                var input = Helper.CastObject<workout_category>(wc);
                var res = _workoutCategoryService.Create(input);
                MapCategory(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("deleteCategory")]
        public HttpResponseMessage DeleteCategory(WorkoutCategory wc)
        {
            try
            {
                var input = Helper.CastObject<workout_category>(wc);
                _workoutCategoryService.DeleteWorkoutCategory(input);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(wc);
        }

        [HttpPost]
        [Route("updateCategory")]
        public HttpResponseMessage UpdateCategory(WorkoutCategory wc)
        {
            WorkoutCategory result = new WorkoutCategory();
            try
            {
                wc.workout_collection = null;
                var input = Helper.CastObject<workout_category>(wc);
                var res = _workoutCategoryService.UpdateWorkoutCategory(input);
                MapCategory(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("getWorkoutsByUser")]
        public HttpResponseMessage GetAllWorkOutsByUser(User usr)
        {
            IEnumerable<WorkoutCollection> result = null;
            try
            {
                var res = _workoutService.GetWorkoutsByUser(usr.user_name);
                result = Helper.CastObject<IEnumerable<WorkoutCollection>>(res);

            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("getWorkout")]
        public HttpResponseMessage GetWorkout(WorkoutCollection wc)
        {
            WorkoutCollection result = new WorkoutCollection();
            try
            {
                var res = _workoutService.GetWorkout(wc.workout_id);
                MapCollection(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("getActiveWorkout")]
        public HttpResponseMessage GetActiveWorkout(WorkoutCollection wc)
        {
            WorkoutActive result = new WorkoutActive();
            try
            {
                var res = _workoutService.GetActiveWorkouts(wc.workout_id);
                MapActive(res.FirstOrDefault(), ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("updateWorkout")]
        public HttpResponseMessage UpdateWorkout(WorkoutCollection wc)
        {
            WorkoutCollection result = new WorkoutCollection();
            try
            {
                var input = Helper.CastObject<workout_collection>(wc);
                var res = _workoutService.UpdateWorkout(input);
                MapCollection(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("addWorkout")]
        public HttpResponseMessage AddWorkout(WorkoutCollection wc)
        {
            WorkoutCollection result = new WorkoutCollection();
            try
            {
                var input = Helper.CastObject<workout_collection>(wc);
                var res = _workoutService.Create(input);
                MapCollection(res, ref result);

            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("deleteWorkout")]
        public HttpResponseMessage DeleteWorkout(WorkoutCollection wc)
        {
            try
            {
                var input = Helper.CastObject<workout_collection>(wc);
                _workoutService.DeleteWorkout(input);

            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(wc);
        }

        [HttpPost]
        [Route("startWorkout")]
        public HttpResponseMessage StartWorkout(WorkoutActive wa)
        {
            WorkoutActive activeWorkout = new WorkoutActive();
            WorkoutCollection result = new WorkoutCollection();
            try
            {
                activeWorkout.workout_id = wa.workout_id;
                activeWorkout.start_date = wa.start_date;
                activeWorkout.start_time = wa.start_time;
                activeWorkout.status = false;
                var input = Helper.CastObject<workout_active>(activeWorkout);
                _workoutService.StartWorkout(input);
                var res  = _workoutService.GetWorkout(activeWorkout.workout_id);
                MapCollection(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        [HttpPost]
        [Route("endWorkout")]
        public HttpResponseMessage EndWorkout(WorkoutActive wa)
        {
            WorkoutActive activeWorkout = new WorkoutActive();
            WorkoutCollection result = new WorkoutCollection();
            try
            {
                activeWorkout.sid = wa.sid;
                activeWorkout.workout_id = wa.workout_id;
                activeWorkout.start_date = wa.start_date;
                activeWorkout.start_time = wa.start_time;
                activeWorkout.end_date = wa.end_date;
                activeWorkout.end_time = wa.end_time;
                activeWorkout.comment = wa.comment;
                activeWorkout.status = true;
                var input = Helper.CastObject<workout_active>(activeWorkout);
                _workoutService.EndWorkout(input);
                var res = _workoutService.GetWorkout(activeWorkout.workout_id);
                MapCollection(res, ref result);
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }

        private void MapCollection(workout_collection source, ref WorkoutCollection des)
        {
            des.workout_id = source.workout_id;
            des.workout_title = source.workout_title;
            des.workout_note = source.workout_note;
            des.category_id = source.category_id;
            des.user_id = source.user_id;
            des.calories_burn_per_min = source.calories_burn_per_min;
            des.workout_category = Helper.CastObject<WorkoutCategory>(source.workout_category);
            des.workout_active = Helper.CastObject<IList<WorkoutActive>>(source.workout_active);
            des.user = Helper.CastObject<User>(source.user);
        }

        private void MapCategory(workout_category source, ref WorkoutCategory des)
        {
            des.category_id = source.category_id;
            des.category_name = source.category_name;
            des.workout_collection = Helper.CastObject<IList<WorkoutCollection>>(source.workout_collection);
        }

        private void MapActive(workout_active source, ref WorkoutActive des)
        {
            des.sid = source.sid;
            des.workout_id = source.workout_id;
            des.start_date = source.start_date;
            des.start_time = source.start_time;
            des.end_date = source.end_date;
            des.end_time = source.end_time;
            des.comment = source.comment;
            des.status = source.status;
            des.workout_collection = Helper.CastObject<WorkoutCollection>(source.workout_collection);
        }

        private void MapUser(user source, ref User des)
        {
            des.user_id = source.user_id;
            des.user_name = source.user_name;
            des.password = source.password;
            des.workout_collection = Helper.CastObject<IList<WorkoutCollection>>(source.workout_collection);
        }
    }
}
