using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;
using WorkoutTracker.Services;
using WorkoutTracker.Common.Exception;
using System.Net.Http;
using System.Web.Http;
using WorkoutTracker.WebApi.Models;
using System.Net;
using System.Diagnostics;

namespace WorkoutTracker.WebApi.Controllers.Tests
{
    [TestClass()]
    public class WorkoutTrackerControllerTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IWorkoutCollectionRepository workoutRepository;
        IWorkoutCollectionService workoutService;
        IWorkoutCategoryRepository categoryRepository;
        IWorkoutCategoryService categoryService;
        IUserRepository userRepository;
        IUserService _userService;
        ILogManager _logManager;

        public WorkoutTrackerControllerTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            workoutRepository = new WorkoutCollectionRepository(_context);
            workoutService = new WorkoutCollectionService(unitOfWork, workoutRepository);
            categoryRepository = new WorkoutCategoryRepository(_context);
            categoryService = new WorkoutCategoryService(unitOfWork, categoryRepository);
            userRepository = new UserRepository(_context);
            _userService = new UserService(unitOfWork, userRepository);
            _logManager = new LogManager();
        }

        [TestMethod()]
        public void A_AddCategoryApiTest()
        {
            WorkoutCategory result;
            workout_category wc = new workout_category()
            {
                category_id = 0,
                category_name = "WebApiTestCategory",
                workout_collection = null
            };
            var input = Helper.CastObject<WorkoutCategory>(wc);
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.AddCategory(input);
            result = response.Content.ReadAsAsync<WorkoutCategory>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }

        [TestMethod()]
        public void B_GetCategoriesApiTest()
        {
            IEnumerable<WorkoutCategory> result = null;
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetCategories();
            result = response.Content.ReadAsAsync<IEnumerable<WorkoutCategory>>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }

        [TestMethod()]
        public void C_GetCategoryApiTest()
        {
            WorkoutCategory result;
            var category_name = "WebApiTestCategory";
            var cat = categoryService.GetWorkoutCategories().Where(c => c.category_name.Equals(category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var input = Helper.CastObject<WorkoutCategory>(cat);
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetCategory(input);
            result = response.Content.ReadAsAsync<WorkoutCategory>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }


        [TestMethod()]
        public void D_UpdateCategoryApiTest()
        {
            WorkoutCategory result;
            var category_name = "WebApiTestCategory";
            var cat = categoryService.GetWorkoutCategories().Where(c => c.category_name.Equals(category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var input = Helper.CastObject<WorkoutCategory>(cat);
            input.category_name = "WebApiTestCategoryUpdated";
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.UpdateCategory(input);
            result = response.Content.ReadAsAsync<WorkoutCategory>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual("WebApiTestCategory", result.category_name);
            response.Dispose();
        }

        [TestMethod()]
        public void E_GetAllWorkOutsByUserApiTest()
        {
            IEnumerable<WorkoutCollection> result;
            var user = _userService.GetUsers().FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            var input = Helper.CastObject<User>(user);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetAllWorkOutsByUser(input);
            result = response.Content.ReadAsAsync<IEnumerable<WorkoutCollection>>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            if (result != null && result.Count() > 0)
            {
                Assert.AreNotEqual(null, result);
                Assert.AreNotEqual(0, result.Count());
            }
            response.Dispose();
        }

        [TestMethod()]
        public void F_AddWorkoutApiTest()
        {
            WorkoutCollection result;
            var category_name = "WebApiTestCategoryUpdated";
            var cat = categoryService.GetWorkoutCategories().Where(c => c.category_name.Equals(category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var user = _userService.GetUsers().FirstOrDefault();
            var catCon = Helper.CastObject<WorkoutCategory>(cat);
            var userCon = Helper.CastObject<User>(user);
            WorkoutCollection wc = new WorkoutCollection()
            {
                workout_id = 0,
                workout_title = "TestApiWorkout",
                workout_note = string.Empty,
                workout_category = catCon,
                workout_active = null,
                calories_burn_per_min = 0,
                category_id = catCon.category_id,
                user = userCon,
                user_id = userCon.user_id
            };
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.AddWorkout(wc);
            result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.workout_id);
            response.Dispose();
        }

        [TestMethod()]
        public void G_GetWorkoutApiTest()
        {
            WorkoutCollection result;
            var wc = workoutService.GetWorkouts().Where(w => w.workout_title.Equals("TestApiWorkout", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            var input = Helper.CastObject<WorkoutCollection>(wc);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetWorkout(input);
            result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }

        [TestMethod()]
        public void H_UpdateWorkoutApiTest()
        {
            WorkoutCollection result;
            var wc = workoutService.GetWorkouts().Where(w => w.workout_title.Equals("TestApiWorkout", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            var input = Helper.CastObject<WorkoutCollection>(wc);
            input.workout_title = "TestApiWorkoutUpdated";
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.UpdateWorkout(input);
            result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }

        [TestMethod()]
        public void I_StartWorkoutApiTest()
        {
            WorkoutCollection result;
            var wc = workoutService.GetWorkouts().Where(w => w.workout_title.Equals("TestApiWorkoutUpdated", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            WorkoutActive wa = new WorkoutActive()
            {
                workout_id = wc.workout_id,
                start_date = DateTime.UtcNow.Date,
                start_time = DateTime.UtcNow.TimeOfDay,
                status = false
            };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.StartWorkout(wa);
            result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(null, result.workout_active);
            Assert.AreNotEqual(0, result.workout_active.Count());
            Assert.AreNotEqual(null, result.workout_active.FirstOrDefault().start_date);
            Assert.AreNotEqual(null, result.workout_active.FirstOrDefault().start_time);
            Assert.AreEqual(false, result.workout_active.FirstOrDefault().status);
            response.Dispose();
        }

        [TestMethod()]
        public void J_EndWorkoutApiTest()
        {
            WorkoutCollection result;
            var wc = workoutService.GetWorkouts().Where(w => w.workout_title.Equals("TestApiWorkoutUpdated", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var waRes = workoutService.GetActiveWorkouts(wc.workout_id).FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            WorkoutActive wa = new WorkoutActive()
            {
                workout_id = wc.workout_id,
                sid = waRes.sid,
                start_date = waRes.start_date,
                start_time = waRes.start_time,
                end_date = DateTime.UtcNow.Date,
                end_time = DateTime.UtcNow.TimeOfDay,
                status = true,
                comment = "Ended"
            };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.EndWorkout(wa);
            result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(null, result.workout_active);
            Assert.AreNotEqual(0, result.workout_active.Count());
            Assert.AreNotEqual(null, result.workout_active.FirstOrDefault().end_date);
            Assert.AreNotEqual(null, result.workout_active.FirstOrDefault().end_time);
            Assert.AreEqual(true, result.workout_active.FirstOrDefault().status);
            Assert.AreNotEqual(null, result.workout_active.FirstOrDefault().comment);
            response.Dispose();
        }

        [TestMethod()]
        public void K_GetActiveWorkoutApiTest()
        {
            WorkoutActive result;
            var wc = workoutService.GetWorkouts().Where(w => w.workout_title.Equals("TestApiWorkoutUpdated", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            var input = Helper.CastObject<WorkoutCollection>(wc);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetActiveWorkout(input);
            result = response.Content.ReadAsAsync<WorkoutActive>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }
        [TestMethod()]
        public void L_DeleteCategoryApiTest()
        {
            WorkoutCategory result;
            var category_name = "WebApiTestCategoryUpdated";
            var cat = categoryService.GetWorkoutCategories().Where(c => c.category_name.Equals(category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var input = Helper.CastObject<WorkoutCategory>(cat);
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.DeleteCategory(input);
            result = response.Content.ReadAsAsync<WorkoutCategory>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }

        [TestMethod()]
        public void M_DeleteWorkoutApiTest()
        {
            WorkoutCollection result;
            var wc = workoutService.GetWorkouts().Where(w => w.workout_title.Equals("TestApiWorkoutUpdated", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
            var input = Helper.CastObject<WorkoutCollection>(wc);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.DeleteWorkout(input);
            result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, result);
            response.Dispose();
        }

        [TestMethod()]
        public void N_WorkoutAddPerformanceTest()
        {
            int noOfCalls = 500;
            double expectedTime = 1;
            WorkoutCollection result;
            var cat = categoryService.GetWorkoutCategories().FirstOrDefault();
            var user = _userService.GetUsers().FirstOrDefault();
            var catCon = Helper.CastObject<WorkoutCategory>(cat);
            var userCon = Helper.CastObject<User>(user);
            var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < noOfCalls; i++)
            {
                WorkoutCollection wc = new WorkoutCollection()
                {
                    workout_id = 0,
                    workout_title = "TestApiWorkout_LoadTest" + i.ToString(),
                    workout_note = "MockUser",
                    workout_category = catCon,
                    workout_active = null,
                    calories_burn_per_min = i * 10,
                    category_id = catCon.category_id,
                    user = userCon,
                    user_id = userCon.user_id
                };
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();
                controller.Request.Headers.Add("Accept", "application/json");
                var response = controller.AddWorkout(wc);
                result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
                response.Dispose();
            }
            stopwatch.Stop();
            var res = stopwatch.Elapsed.TotalMinutes <= expectedTime;
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void O_WorkoutLoadTest()
        {
            double expectedTime = 1;
            var locker = new Object();
            int count = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Threading.Tasks.Parallel.For
                (0
             , 1000
             , new System.Threading.Tasks.ParallelOptions { MaxDegreeOfParallelism = 5 }
             , (i) =>
             {
                 System.Threading.Interlocked.Increment(ref count);
                 lock (locker)
                 {
                     IEnumerable<WorkoutCollection> result;
                     var user = _userService.GetUsers().FirstOrDefault();
                     var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
                     var input = Helper.CastObject<User>(user);
                     controller.Request = new HttpRequestMessage();
                     controller.Configuration = new HttpConfiguration();
                     controller.Request.Headers.Add("Accept", "application/json");
                     var response = controller.GetAllWorkOutsByUser(input);
                     result = response.Content.ReadAsAsync<IEnumerable<WorkoutCollection>>().Result;
                     response.Dispose();
                     System.Threading.Thread.Sleep(10);
                 }
                 System.Threading.Interlocked.Decrement(ref count);
             }
            );

            stopwatch.Stop();
            var res = stopwatch.Elapsed.TotalMinutes <= expectedTime;
            Assert.IsTrue(res);
        }

        [TestMethod()]
        public void P_WorkoutDeletePerformanceTest()
        {
            double expectedTime = 1;
            WorkoutCollection result;
            var workouts = workoutService.GetWorkouts().Where(w => w.workout_note.Equals("MockUser", StringComparison.InvariantCultureIgnoreCase)).ToList();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (workouts != null && workouts.Count() > 0)
            {
                System.Threading.Tasks.Parallel.ForEach(workouts, workout =>
                {
                    var controller = new WorkoutTrackerController(workoutService, categoryService, _logManager);
                    var input = Helper.CastObject<WorkoutCollection>(workout);
                    controller.Request = new HttpRequestMessage();
                    controller.Configuration = new HttpConfiguration();
                    controller.Request.Headers.Add("Accept", "application/json");
                    var response = controller.DeleteWorkout(input);
                    result = response.Content.ReadAsAsync<WorkoutCollection>().Result;
                    response.Dispose();

                });
            }

            stopwatch.Stop();
            var res = stopwatch.Elapsed.TotalMinutes <= expectedTime;
            Assert.IsTrue(res);
        }
    }
}