using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using JimJenkins.GeoCoding.Services;
using JimJenkins.Weather.WeatherGov.DataService;

namespace Web.IoC
{
    public static class AutoFacContainerBuilder
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            RegisterForApplication(builder);
            RegisterForMvc(builder);
            RegisterForWebApi(builder);

            var container = builder.Build();
            // Set the dependency resolver to be Autofac.
            //mvc
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //web api
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

        private static void RegisterForWebApi(ContainerBuilder builder)
        {
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);            
            
        }

        private static void RegisterForMvc(ContainerBuilder builder)
        {
            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

        }

        private static void RegisterForApplication(ContainerBuilder builder)
        {
            //geocoding
            var assembly = typeof (IGeoCodingService).Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

            builder.RegisterType<GeoCodingServiceConfigProvider>()
                .As<IGeoCodingServiceConfigProvider>()
                .WithParameter(new NamedParameter("serviceKey",
                    ConfigurationManager.AppSettings["googleServerKey"]));

            //builder.RegisterType(typeof (GeoCodingService))
            //    .As<IGeoCodingService>().InstancePerRequest();

            //weather data
            assembly = typeof (IWeatherService).Assembly;
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();
        }
    }
}