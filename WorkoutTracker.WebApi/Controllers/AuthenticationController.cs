using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WorkoutTracker.Common.Exception;
using WorkoutTracker.Entities;
using WorkoutTracker.Services;
using WorkoutTracker.WebApi.Models;

namespace WorkoutTracker.WebApi.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthenticationController : BaseAPIController
    {
        private IUserService _userService;
        private ILogManager _logManager;
        public AuthenticationController(IUserService userService, ILogManager logManager)
        {
            _userService = userService;
            _logManager = logManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public HttpResponseMessage Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginrequest = new LoginRequest { };
            user usr = new user();

            bool isUsernamePasswordValid = false;

            if (login != null)
            {
                loginrequest.UserName = login.UserName.ToLower();
                loginrequest.Password = login.Password;
                usr = CheckUser(login.UserName, login.Password);
                if (usr != null)
                    isUsernamePasswordValid = true;
            }
      
            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                string token = JwtManager.GenerateToken(loginrequest.UserName);
                //return the token
                loginResponse.Token = token;
                loginResponse.UserName = loginrequest.UserName;
                loginResponse.UserID = usr.user_id;
                return ToJson(loginResponse);
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
        private user CheckUser(string username, string password)
        {
            var user = _userService.GetUser(username, password);
            return user;
        }
        //private string createToken(string username)
        //{
        //    //Set issued at date
        //    DateTime issuedAt = DateTime.UtcNow;
        //    double expirationInMinutes = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["TokenExpiryInMinutes"].ToString());
        //    //set the time when it expires
        //    DateTime expires = DateTime.UtcNow.AddMinutes(expirationInMinutes);

        //    //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    //create a identity and add claims to the user which we want to log in
        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.Name, username)
        //    });

        //    string sec = System.Configuration.ConfigurationManager.AppSettings["JWTSecurityKey"].ToString();
        //    var now = DateTime.UtcNow;
        //    var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
        //    var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);


        //    //create the jwt
        //    var token =
        //        (JwtSecurityToken)
        //            tokenHandler.CreateJwtSecurityToken(issuer: System.Configuration.ConfigurationManager.AppSettings["Issuer"].ToString(), audience: System.Configuration.ConfigurationManager.AppSettings["Audience"].ToString(),
        //                subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
        //    var tokenString = tokenHandler.WriteToken(token);

        //    return tokenString;
        //}
    }
}
