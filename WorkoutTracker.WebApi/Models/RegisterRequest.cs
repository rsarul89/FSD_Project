﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkoutTracker.WebApi.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}