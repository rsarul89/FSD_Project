using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;

namespace WorkoutTracker.Services.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IUserRepository userRepository;
        IUserService userService;
        public  UserServiceTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            userRepository = new UserRepository(_context);
            userService = new UserService(unitOfWork, userRepository);
        }

        [TestMethod()]
        public void A_CreateUserTest()
        {
            user _user = new user()
            {
                user_id = 0,
                user_name = "TestServiceUser",
                password = "Demopassword",
                workout_collection = null

            };
            var usrList = userService.GetUsers().Where(x => x.user_name.Equals(_user.user_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            { }
            else
            {
                userService.CreateUser(_user);
                var result = userService.GetUsers().Where(x => x.user_name.Equals(_user.user_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreNotEqual(0, result.user_id);
            }
        }

        [TestMethod()]
        public void B_GetUserTest()
        {
            var user = userService.GetUserByUserName("TestServiceUser");
            Assert.AreNotEqual(null, user);
        }

        [TestMethod()]
        public void C_GetUserByUserNameTest()
        {
            var user = userService.GetUserByUserName("TestServiceUser");
            Assert.AreNotEqual(null, user);
        }

        [TestMethod()]
        public void D_GetUsersTest()
        {
            var users = userService.GetUsers().ToList();
            Assert.AreNotEqual(null, users);
            Assert.AreNotEqual(0, users.Count());
        }

        [TestMethod()]
        public void E_UpdateUserTest()
        {
            var uname = "TestServiceUser";
            var usrList = userService.GetUsers().Where(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            {
                usrList.password = "DemoPassword1";
                userService.UpdateUser(usrList);
                var result = userService.GetUsers().Where(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual("DemoPassword1", result.password);
            }
        }

        [TestMethod()]
        public void F_DeleteUserTest()
        {
            var uname = "TestServiceUser";
            var usrList = userService.GetUsers().Where(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            {
                userService.DeleteUser(usrList);
                var result = userService.GetUsers().Where(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual(null, result);
            }
        }
    }
}