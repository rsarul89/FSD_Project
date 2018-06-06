using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutTracker.Repositories;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Services.Tests
{
    [TestClass()]
    public class WorkoutCollectionServiceTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IWorkoutCollectionRepository workoutCollectionRepository;
        IWorkoutCategoryRepository workoutCategoryRepository;
        IUserRepository userRepository;
        IWorkoutCollectionService workoutCollectionService;
        IWorkoutCategoryService workoutCategoryService;
        IUserService userService;
        public WorkoutCollectionServiceTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            workoutCollectionRepository = new WorkoutCollectionRepository(_context);
            workoutCategoryRepository = new WorkoutCategoryRepository(_context);
            userRepository = new UserRepository(_context);
            workoutCollectionService = new WorkoutCollectionService(unitOfWork, workoutCollectionRepository);
            workoutCategoryService = new WorkoutCategoryService(unitOfWork, workoutCategoryRepository);
            userService = new UserService(unitOfWork, userRepository);
        }

        [TestMethod()]
        public void A_CreateWorkoutTest()
        {
            var categories = workoutCategoryService.GetWorkoutCategories().FirstOrDefault();
            var users = userService.GetUsers().FirstOrDefault();
            workout_collection wc = new workout_collection()
            {
                workout_id = 0,
                workout_title = "TestServiceWorkout",
                workout_note = "TestServiceNote",
                category_id = categories.category_id,
                calories_burn_per_min = 50,
                user_id = users.user_id,
                user = users,
                workout_category = categories,
                workout_active = null

            };
            var wrList = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals(wc.workout_title, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (wrList != null)
            { }
            else
            {
                workoutCollectionService.CreateWorkout(wc);
                var result = workoutCollectionRepository.FindBy(x => x.workout_title.Equals(wc.workout_title, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreNotEqual(0, result.workout_id);
            }
        }

        [TestMethod()]
        public void B_StartWorkoutTest()
        {
            int wId = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
              .FirstOrDefault().workout_id;
            workout_active wa = new workout_active()
            {
                workout_id = wId,
                start_date = DateTime.UtcNow.Date,
                start_time = DateTime.UtcNow.TimeOfDay,
                status = false
            };
            workoutCollectionService.StartWorkout(wa);
            var result = workoutCollectionService.GetActiveWorkouts(wId).ToList();
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count());
            Assert.AreEqual(false, wa.status);
            Assert.AreEqual(null, wa.comment);
            Assert.AreNotEqual(null, wa.start_date);
            Assert.AreNotEqual(null, wa.start_time);
            Assert.AreEqual(null, wa.end_date);
            Assert.AreEqual(null, wa.end_time);
        }

        [TestMethod()]
        public void C_EndWorkoutTest()
        {
            var wc = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
              .FirstOrDefault();
            var result = workoutCollectionService.GetActiveWorkouts(wc.workout_id).ToList();
            workout_active wa = new workout_active()
            {
                workout_id = wc.workout_id,
                sid = result.LastOrDefault().sid,
                start_date = result.FirstOrDefault().start_date,
                start_time = result.FirstOrDefault().start_time,
                end_date = DateTime.UtcNow.Date,
                end_time = DateTime.UtcNow.TimeOfDay,
                comment = "Ended",
                status = true,

            };
            var result1 = workoutCollectionService.GetActiveWorkouts(wc.workout_id).ToList().Where(a => a.sid == wa.sid);
            workoutCollectionService.EndWorkout(wa);
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count());
            Assert.AreEqual(true, result1.FirstOrDefault().status);
            Assert.AreNotEqual(null, result1.FirstOrDefault().comment);
            Assert.AreNotEqual(null, result1.FirstOrDefault().end_date);
            Assert.AreNotEqual(null, result1.FirstOrDefault().end_time);
        }

        [TestMethod()]
        public void D_GetActiveWorkoutsTest()
        {
            int wId = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().workout_id;
            var result = workoutCollectionService.GetActiveWorkouts(wId).ToList();
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod()]
        public void E_GetWorkoutsTest()
        {
            int wId = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
               .FirstOrDefault().workout_id;
            var result = workoutCollectionService.GetWorkout(wId);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod()]
        public void F_GetWorkoutsByUserTest()
        {
            var users = userService.GetUsers().FirstOrDefault();
            var results = workoutCollectionService.GetWorkoutsByUser(users.user_name).ToList();
            if (results != null)
            {
                Assert.AreNotEqual(null, results);
                Assert.AreNotEqual(0, results.Count());
            }
        }

        [TestMethod()]
        public void G_GetWorkoutTest()
        {
            int wId = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().workout_id;
            var result = workoutCollectionService.GetWorkout(wId);
            Assert.AreNotEqual(null, result);
        }

        [TestMethod()]
        public void H_UpdateWorkoutTest()
        {
            workout_collection wc = new workout_collection();
            var workout = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if (workout != null)
            {
                workout.workout_note = "TestServiceNote1";
                wc = workoutCollectionService.UpdateWorkout(workout);
                Assert.AreNotEqual("TestServiceNote", wc.workout_note);
            }
        }

        [TestMethod()]
        public void I_DeleteWorkoutTest()
        {
            var workout = workoutCollectionService.GetWorkouts().Where(x => x.workout_title.Equals("TestServiceWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if (workout != null)
            {
                workoutCollectionService.DeleteWorkout(workout);
                var result = workoutCollectionService.GetWorkout(workout.workout_id);
                Assert.AreEqual(null, result);
            }
        }
    }
}