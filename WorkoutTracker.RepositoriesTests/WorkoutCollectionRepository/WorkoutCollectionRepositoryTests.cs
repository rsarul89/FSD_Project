using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories.Tests
{
    [TestClass()]
    public class WorkoutCollectionRepositoryTests
    {
        private readonly WorkoutTrackerEntities _context;
        IWorkoutCollectionRepository wcRepository;
        IWorkoutCategoryRepository workoutCategoryRepository;
        IUserRepository userRepository;
        public WorkoutCollectionRepositoryTests()
        {
            _context = new WorkoutTrackerEntities();
            wcRepository = new WorkoutCollectionRepository(_context);
            workoutCategoryRepository = new WorkoutCategoryRepository(_context);
            userRepository = new UserRepository(_context);
        }

        [TestMethod()]
        public void A_AddWorkoutTest()
        {
            var categories = workoutCategoryRepository.GetCategories().FirstOrDefault();
            var users = userRepository.GetAll().FirstOrDefault();
            workout_collection wc = new workout_collection()
            {
                workout_id = 0,
                workout_title = "TestRepositoryWorkout",
                workout_note = "TestRepositoryNote",
                category_id = categories.category_id,
                calories_burn_per_min = 50,
                user_id = users.user_id,
                user = users,
                workout_category = categories,
                workout_active = null

            };
            var usrList = wcRepository.FindBy(x => x.workout_title.Equals(wc.workout_title, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            { }
            else
            {
                wcRepository.AddWorkout(wc);
                _context.SaveChanges();
                var result = wcRepository.FindBy(x => x.workout_title.Equals(wc.workout_title, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreNotEqual(0, result.workout_id);
            }
        }

        [TestMethod()]
        public void B_StartWorkoutTest()
        {
            int wId = wcRepository.GetAll().Where(x => x.workout_title.Equals("TestRepositoryWorkout", StringComparison.InvariantCultureIgnoreCase))
               .FirstOrDefault().workout_id;
            workout_active wa = new workout_active()
            {
                workout_id = wId,
                start_date = DateTime.UtcNow.Date,
                start_time = DateTime.UtcNow.TimeOfDay,
                status = false

            };
            wcRepository.StartWorkout(wa);
            _context.SaveChanges();
            var result = wcRepository.GetActiveWorkouts(wId).ToList();
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
            var wc= wcRepository.GetAll().Where(x => x.workout_title.Equals("TestRepositoryWorkout", StringComparison.InvariantCultureIgnoreCase))
               .FirstOrDefault();
            var result = wcRepository.GetActiveWorkouts(wc.workout_id).ToList();
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
            var result1 = wcRepository.GetActiveWorkouts(wc.workout_id).ToList();
            wcRepository.EndWorkout(wa);
            _context.SaveChanges();
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
            int wId = wcRepository.GetAll().Where(x => x.workout_title.Equals("TestRepositoryWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().workout_id;
            var result = wcRepository.GetActiveWorkouts(wId).ToList();
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod()]
        public void E_GetAllWorkoutsByUserTest()
        {
            var users = userRepository.GetAll().FirstOrDefault();
            var results = wcRepository.GetAllWorkoutsByUser(users.user_name).ToList();
            Assert.AreNotEqual(null, results);
            Assert.AreNotEqual(0, results.Count());
        }

        [TestMethod()]
        public void F_GetWorkoutTest()
        {
            int wId = wcRepository.GetAll().Where(x => x.workout_title.Equals("TestRepositoryWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault().workout_id;
            var result = wcRepository.GetWorkout(wId);
            Assert.AreNotEqual(null, result);
        }
        [TestMethod()]
        public void G_UpdateWorkoutTest()
        {
            workout_collection wc = new workout_collection();
            var workout = wcRepository.GetAll().Where(x => x.workout_title.Equals("TestRepositoryWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if(workout != null)
            {
                workout.workout_note = "TestRepositoryNote1";
                wc = wcRepository.UpdateWorkout(workout);
                _context.SaveChanges();
                Assert.AreNotEqual("TestRepositoryNote", wc.workout_note);
            }
        }

        [TestMethod()]
        public void H_DeleteWorkoutTest()
        {
            var workout = wcRepository.GetAll().Where(x => x.workout_title.Equals("TestRepositoryWorkout", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            if (workout != null)
            {
                wcRepository.DeleteWorkout(workout);
                _context.SaveChanges();
                var result = wcRepository.GetWorkout(workout.workout_id);
                Assert.AreEqual(null, result);
            }
        }
    }
}