using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Microsoft.Owin;
using WorkoutTracker.Common.Exception;
using System.Reflection;
[assembly:OwinStartup(typeof(WorkoutTracker.WebApi.Startup))]
namespace WorkoutTracker.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            // Web API configuration and services
            WebApiConfig.Register(config);
            // Autofac configuration
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new EFModule());
            builder.RegisterType<LogManager>().As<ILogManager>().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(IWorkoutCategoryRepository).Assembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterAssemblyTypes(typeof(IWorkoutCollectionRepository).Assembly)
            //     .Where(t => t.Name.EndsWith("Repository"))
            //     .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterAssemblyTypes(typeof(IUserRepository).Assembly)
            //  .Where(t => t.Name.EndsWith("Repository"))
            //  .AsImplementedInterfaces().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(IWorkoutCategoryService).Assembly)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterAssemblyTypes(typeof(IWorkoutCollectionService).Assembly)
            //     .Where(t => t.Name.EndsWith("Service"))
            //     .AsImplementedInterfaces().InstancePerRequest();
            //builder.RegisterAssemblyTypes(typeof(IUserService).Assembly)
            //    .Where(t => t.Name.EndsWith("Service"))
            //    .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
        }
    }
}