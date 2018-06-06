using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WorkoutTracker.Entities;
using WorkoutTracker.Repositories;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.Services;
using WorkoutTracker.WebApi.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace WorkoutTracker.WebApi.Controllers.Tests
{
    [TestClass()]
    public class ReportControllerTests
    {
        private readonly WorkoutTrackerEntities _context;
        IUnitOfWork unitOfWork;
        IWorkoutCollectionRepository workoutRepository;
        IWorkoutCollectionService workoutService;
        IUserRepository userRepository;
        IUserService _userService;
        ILogManager _logManager;

        public ReportControllerTests()
        {
            _context = new WorkoutTrackerEntities();
            unitOfWork = new UnitOfWork(_context);
            workoutRepository = new WorkoutCollectionRepository(_context);
            workoutService = new WorkoutCollectionService(unitOfWork, workoutRepository);
            userRepository = new UserRepository(_context);
            _userService = new UserService(unitOfWork, userRepository);
            _logManager = new LogManager();
        }

        [TestMethod()]
        public void GetWeeklyReportTest()
        {
            var controller = new ReportController(workoutService, _logManager);
            var user = _userService.GetUsers().FirstOrDefault();
            var result = Helper.CastObject<User>(user);
            Report res;
            Report rpt = new Report()
            {
                IncrId = 0,
                Next = 0,
                Prev = 0,
                IsPrev = false,
                User = result,
                FromDate = string.Empty,
                ToDate = string.Empty,
                Data = null
            };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetWeeklyReport(rpt);
            res = response.Content.ReadAsAsync<Report>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, res);
        }

        [TestMethod()]
        public void GetMonthlyReportTest()
        {
            var controller = new ReportController(workoutService, _logManager);
            var user = _userService.GetUsers().FirstOrDefault();
            var result = Helper.CastObject<User>(user);
            Report res;
            Report rpt = new Report()
            {
                IncrId = 0,
                Next = 0,
                Prev = 0,
                IsPrev = false,
                User = result,
                FromDate = string.Empty,
                ToDate = string.Empty,
                Data = null
            };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetMonthlyReport(rpt);
            res = response.Content.ReadAsAsync<Report>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, res);
        }

        [TestMethod()]
        public void GetYearlyReportTest()
        {
            var controller = new ReportController(workoutService, _logManager);
            var user = _userService.GetUsers().FirstOrDefault();
            var result = Helper.CastObject<User>(user);
            Report res;
            Report rpt = new Report()
            {
                IncrId = 0,
                Next = 0,
                Prev = 0,
                IsPrev = false,
                User = result,
                FromDate = string.Empty,
                ToDate = string.Empty,
                Data = null
            };
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            controller.Request.Headers.Add("Accept", "application/json");
            var response = controller.GetYearlyReport(rpt);
            res = response.Content.ReadAsAsync<Report>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreNotEqual(null, res);
        }
    }
}