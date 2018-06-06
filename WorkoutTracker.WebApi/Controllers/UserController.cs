using System;
using System.Net.Http;
using System.Web.Http;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.Entities;
using WorkoutTracker.Services;
using WorkoutTracker.WebApi.Models;

namespace WorkoutTracker.WebApi.Controllers
{
    [RoutePrefix("api/user")]
    [Authentication]
    public class UserController : BaseAPIController
    {
        private IUserService _userService;
        private ILogManager _logManager;
        public UserController(IUserService userService, ILogManager logManager)
        {
            _userService = userService;
            _logManager = logManager;
        }
        [Route("register")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register(RegisterRequest user)
        {
            user result = new user();
            try
            {
                var usr = new user();
                usr.user_name = user.Username;
                usr.password = user.Password;
                var ur = _userService.GetUserByUserName(user.Username);
                if (ur != null)
                {
                    result = null;
                }
                else
                {
                    _userService.CreateUser(usr);
                    result = _userService.GetUserByUserName(user.Username);
                }
            }
            catch (Exception ex)
            {
                _logManager.WriteLog(ex);
            }
            return ToJson(result);
        }
    }
}
