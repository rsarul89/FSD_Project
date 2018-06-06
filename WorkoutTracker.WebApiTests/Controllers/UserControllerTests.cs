using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutTracker.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;
using WorkoutTracker.Services;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.WebApi.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace WorkoutTracker.WebApi.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IUserRepository userRepository;
        IUserService _userService;
        ILogManager _logManager;
        public UserControllerTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            userRepository = new UserRepository(_context);
            _userService = new UserService(unitOfWork, userRepository);
            _logManager = new LogManager();
        }

        [TestMethod()]
        public void RegisterTest()
        {
            user result;
            RegisterRequest registerRequest = new RegisterRequest { };
            registerRequest.Username = "ApiTestUser";
            registerRequest.Password = "demopassword";
            var controller = new UserController(_userService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var exist = _userService.GetUsers().Where(x => x.user_name.Equals("ApiTestUser", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (exist == null)
            {
                var response = controller.Register(registerRequest);
                result = response.Content.ReadAsAsync<user>().Result;
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                Assert.AreNotEqual(null, result);
                response.Dispose();
                var usr = _userService.GetUsers().Where(x => x.user_name.Equals("ApiTestUser", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                _userService.DeleteUser(usr);
            }
            else
            {
                _userService.DeleteUser(exist);
            }
        }
    }
}