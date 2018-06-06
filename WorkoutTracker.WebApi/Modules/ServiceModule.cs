﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WorkoutTracker.WebApi
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("WorkoutTracker.Services"))
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }
    }
}