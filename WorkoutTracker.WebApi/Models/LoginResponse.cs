using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {

            this.Token = "";
            this.UserName = "";
        }

        public string Token { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }

    }
}