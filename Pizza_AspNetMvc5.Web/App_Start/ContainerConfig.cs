using Autofac;
using Autofac.Integration.Mvc;
using Pizza_AspNetMvc5.Data.Services;
using System.Web.Mvc;

namespace Pizza_AspNetMvc5.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<InMemoryPizzeriaData>()
                .As<IPizzeriaData>()
                .SingleInstance();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}