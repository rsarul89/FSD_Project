using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WorkoutTracker.Entities;

namespace WorkoutTracker.Repositories.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUserRepository userRepository;

        public UserRepositoryTests()
        {
            _context = new WorkoutTrackerEntities();
            userRepository = new UserRepository(_context);
        }
        [TestMethod()]
        public void A_AddUserTest()
        {
            user _user = new user()
            {
                user_id = 0,
                user_name = "TestRepositoryUser",
                password = "Demopassword",
                workout_collection = null

            };
            var usrList = userRepository.FindBy(x => x.user_name.Equals(_user.user_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            { }
            else
            {
                userRepository.AddUser(_user);
                _context.SaveChanges();
                var result = userRepository.FindBy(x => x.user_name.Equals(_user.user_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreNotEqual(0, result.user_id);
            }
        }
        [TestMethod()]
        public void B_UserAlreadyExistsTest()
        {
            user _user = new user()
            {
                user_id = 0,
                user_name = "TestRepositoryUser",
                password = "Demopassword",
                workout_collection = null

            };
            var usrList = userRepository.FindBy(x => x.user_name.Equals(_user.user_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            { }
            else
            {
                userRepository.AddUser(_user);
                _context.SaveChanges();
                var result = userRepository.FindBy(x => x.user_name.Equals(_user.user_name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual(_user.user_name, result.user_name);
            }
        }

        [TestMethod()]
        public void C_UpdateUserTest()
        {
            var uname = "TestRepositoryUser";
            var usrList = userRepository.FindBy(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            {
                usrList.password = "DemoPassword1";
                userRepository.UpdateUser(usrList);
                _context.SaveChanges();
                var result = userRepository.FindBy(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual("DemoPassword1", result.password);
            }
        }

        [TestMethod()]
        public void D_DeleteUserTest()
        {
            var uname = "TestRepositoryUser";
            var usrList = userRepository.FindBy(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (usrList != null)
            {
                userRepository.DeleteUser(usrList);
                _context.SaveChanges();
                var result = userRepository.FindBy(x => x.user_name.Equals(uname, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                Assert.AreEqual(null, result);
            }
        }
    }
}