using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;

namespace WorkoutTracker.Services.Tests
{
    [TestClass()]
    public class WorkoutCategoryServiceTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IWorkoutCategoryRepository workoutCategoryRepository;
        IWorkoutCategoryService workoutCategoryService;

        public WorkoutCategoryServiceTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            workoutCategoryRepository = new WorkoutCategoryRepository(_context);
            workoutCategoryService = new WorkoutCategoryService(unitOfWork, workoutCategoryRepository);
        }

        [TestMethod()]
        public void A_CreateWorkoutCategoryTest()
        {
            workout_category wcat = new workout_category()
            {
                category_id = 0,
                category_name = "TestServicecategory",
                workout_collection = null

            };
            var catList = workoutCategoryService.GetWorkoutCategories().Where(x => x.category_name.Equals(wcat.category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            { }
            else
            {
                workoutCategoryService.CreateWorkoutCategory(wcat);
            }
            var result = workoutCategoryService.GetWorkoutCategories().Where(x => x.category_name.Equals(wcat.category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            Assert.AreNotEqual(0, result.category_id);
        }

        [TestMethod()]
        public void B_GetCategoryTest()
        {
            workout_category wc = null;
            var catList = workoutCategoryService.GetWorkoutCategories().Where(x => x.category_name.Equals("TestServicecategory", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            {
                wc = new workout_category();
                wc = workoutCategoryService.GetCategory(catList.category_id);
            }

            Assert.AreNotEqual(null, wc);
        }

        [TestMethod()]
        public void C_GetWorkoutCategoriesTest()
        {
            var result = workoutCategoryService.GetWorkoutCategories();
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod()]
        public void D_UpdateWorkoutCategoryTest()
        {
            var catName = "TestServicecategory";
            var catList = workoutCategoryService.GetWorkoutCategories().Where(x => x.category_name.Equals(catName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            {
                catList.category_name = "TestServicecategoryUpdated";
                workoutCategoryService.UpdateWorkoutCategory(catList);
                var result = workoutCategoryService.GetAll().Where(x => x.category_name.Equals("TestServicecategoryUpdated", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreNotEqual("TestServicecategory", result.category_name);
            }
        }

        [TestMethod()]
        public void E_DeleteWorkoutCategoryTest()
        {
            var catName = "TestServicecategoryUpdated";
            var catList = workoutCategoryService.GetWorkoutCategories().Where(x => x.category_name.Equals(catName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            {
                workoutCategoryService.DeleteWorkoutCategory(catList);
                var result = workoutCategoryService.GetAll().Where(x => x.category_name.Equals(catName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual(null, result);
            }
        }
    }
}