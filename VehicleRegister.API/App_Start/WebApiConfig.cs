using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VehicleRegister.Domain.Interfaces;
using VehicleRegister.Domain.Classes;
using SimpleInjector.Integration.WebApi;
using VehicleRegister.Repository.Interfaces;
using VehicleRegister.Repository.Classes;

namespace VehicleRegister.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterSimpleInjector(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<IVehicleService, VehicleService>(Lifestyle.Scoped);
            container.Register<IVehicleRepository, AzureRepository>(Lifestyle.Scoped);
            container.Register<IAccountRepository, AzureRepository>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
