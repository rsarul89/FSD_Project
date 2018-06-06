using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkoutTracker.Services;
using WorkoutTracker.Repositories;
using WorkoutTracker.Entities;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.WebApi.Models;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace WorkoutTracker.WebApi.Controllers.Tests
{
    [TestClass()]
    public class AuthenticationControllerTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IUserRepository userRepository;
        IUserService _userService;
        ILogManager _logManager;
        public AuthenticationControllerTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            userRepository = new UserRepository(_context);
            _userService = new UserService(unitOfWork, userRepository);
            _logManager = new LogManager();
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            LoginRequest loginrequest = new LoginRequest { };
            LoginResponse loginResponse;
            loginrequest.UserName = "demouser1";
            loginrequest.Password = "demopassword";
            var controller = new AuthenticationController(_userService, _logManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Authenticate(loginrequest);
            loginResponse = response.Content.ReadAsAsync<LoginResponse>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, loginResponse.Token);
            Assert.AreNotEqual(null, loginResponse.UserID);
            Assert.AreNotEqual(null, loginResponse.UserName);
        }
    }
}