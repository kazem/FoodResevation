using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Data;
using Entities;
using Services;

namespace WebFramework.Configuration
{
    public static class AutofacConfigurationExtensions
    {
        //public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        //{
        //    var containerBuilder = new ContainerBuilder();
        //    containerBuilder.Populate(services);
        //    containerBuilder.AddServices();


        //    var container = containerBuilder.Build();
        //    return new AutofacServiceProvider(container);
        //}

        public static void AddServices(this ContainerBuilder container)
        {
            container.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            var commonAssembly = typeof(SiteSettings).Assembly;
            var dataAssembly = typeof(ApplicationDbContext).Assembly;
            var entityAssembly = typeof(IEntity).Assembly;
            var servicesAssembly = typeof(JwtService).Assembly;

            container.RegisterAssemblyTypes(commonAssembly, dataAssembly, entityAssembly, servicesAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            container.RegisterAssemblyTypes(commonAssembly, dataAssembly, entityAssembly, servicesAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            container.RegisterAssemblyTypes(commonAssembly, dataAssembly, entityAssembly, servicesAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
