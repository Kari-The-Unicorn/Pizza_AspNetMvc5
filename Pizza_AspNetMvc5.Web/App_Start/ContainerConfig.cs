using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Pizza_AspNetMvc5.Data;
using System.Web.Http;
using System.Web.Mvc;

namespace Pizza_AspNetMvc5.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<InMemoryPizzeriaData>()
                .As<IPizzeriaData>()
                .SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}