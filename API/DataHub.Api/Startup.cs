using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using DataHub.Data;
using DataHub.Domain.Entities;
using Microsoft.Owin;
using Owin;
using NSwag.AspNet.Owin;

[assembly: OwinStartup(typeof(DataHub.Api.Startup))]

namespace DataHub.Api
{
    /// <summary>
    /// Owin automatic startup.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configure Owin.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register components here
            builder.RegisterType<DataHubContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<DataHubEfRepository>().As<IDataHubEfRepository>().InstancePerRequest();

            var container = builder.Build();

            var config = WebApiConfig.Register();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseSwaggerUi(typeof(Startup).Assembly, new SwaggerUiOwinSettings());

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

            config.EnsureInitialized();
        }
    }
}
