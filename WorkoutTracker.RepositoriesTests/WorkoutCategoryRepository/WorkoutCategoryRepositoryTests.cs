using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories.Tests
{
    [TestClass()]
    public class WorkoutCategoryRepositoryTests
    {
        private readonly WorkoutTrackerEntities _context;
        IWorkoutCategoryRepository workoutCategoryRepository;
        public WorkoutCategoryRepositoryTests()
        {
            _context = new WorkoutTrackerEntities();
            workoutCategoryRepository = new WorkoutCategoryRepository(_context);
        }

        [TestMethod()]
        public void A_AddCategoryTest()
        {
            workout_category wcat = new workout_category()
            {
                category_id = 0,
                category_name = "TestRepositorycategory",
                workout_collection = null

            };
            var catList = workoutCategoryRepository.FindBy(x => x.category_name.Equals(wcat.category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            { }
            else
            {
                workoutCategoryRepository.AddCategory(wcat);
                _context.SaveChanges();
            }
            var result = workoutCategoryRepository.FindBy(x => x.category_name.Equals(wcat.category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            Assert.AreNotEqual(0, result.category_id);
        }

        [TestMethod()]
        public void B_CategoryAlreadyExistsTest()
        {
            workout_category wcat = new workout_category()
            {
                category_id = 0,
                category_name = "TestRepositorycategory",
                workout_collection = null

            };
            var catList = workoutCategoryRepository.FindBy(x => x.category_name.Equals(wcat.category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            { }
            else
            {
                workoutCategoryRepository.AddCategory(wcat);
                _context.SaveChanges();
            }
            Assert.AreNotEqual(null, catList);
            Assert.AreEqual(catList.category_name, wcat.category_name);
        }

        [TestMethod()]
        public void C_GetCategoriesTest()
        {
            var result = workoutCategoryRepository.GetCategories();
            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.Count());
        }

        [TestMethod()]
        public void D_GetCategoryTest()
        {
            workout_category wc = null;
            workout_category wcat = new workout_category()
            {
                category_id = 0,
                category_name = "TestRepositorycategory",
                workout_collection = null

            };
            var catList = workoutCategoryRepository.FindBy(x => x.category_name.Equals(wcat.category_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            {
                wc = new workout_category();
                wc = workoutCategoryRepository.GetCategory(catList.category_id);
            }

            Assert.AreNotEqual(null, wc);
        }

        [TestMethod()]
        public void E_UpdateCategoryTest()
        {
            var catName = "TestRepositorycategory";
            var catList = workoutCategoryRepository.FindBy(x => x.category_name.Equals(catName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if(catList != null)
            {
                catList.category_name = "TestRepositorycategoryUpdated";
                workoutCategoryRepository.UpdateCategory(catList);
                _context.SaveChanges();
                var result = workoutCategoryRepository.FindBy(x => x.category_name.Equals("TestRepositorycategoryUpdated", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreNotEqual("TestRepositorycategory", result.category_name);
            }
        }

        [TestMethod()]
        public void F_DeleteCategoryTest()
        {
            var catName = "TestRepositorycategoryUpdated";
            var catList = workoutCategoryRepository.FindBy(x => x.category_name.Equals(catName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (catList != null)
            {
                workoutCategoryRepository.DeleteCategory(catList);
                _context.SaveChanges();
                var result = workoutCategoryRepository.FindBy(x => x.category_name.Equals(catName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual(null, result);
            }
        }
    }
}