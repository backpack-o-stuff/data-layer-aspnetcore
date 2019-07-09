using System;
using System.Collections.Generic;
using System.Reflection;
using DL.Data.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DL.ClientLayer.Infrastructure.IoC
{
    public static class DependencyRegistrations
    {
        private static IServiceProvider _serviceProvider;
        private static readonly Assembly[] AutoResolvedAssemblies = new []
        {
            Assembly.Load("DL.Application"),
            Assembly.Load("DL.Data"),
            Assembly.Load("DL.ClientLayer")
        };

        public static void Register(IServiceCollection services)
        {
            RegisterResolvers(services);
        }

        public static T Resolve<T>()
        {
            return Resolve<T>(new List<Action<IServiceCollection>>());
        }

        public static T Resolve<T>(List<Action<IServiceCollection>> registerResolverOverrides)
        {
            return (T) ServiceProvider(registerResolverOverrides).GetService(typeof(T));
        }

        private static IServiceProvider ServiceProvider(List<Action<IServiceCollection>> registerResolverOverrides)
        {
            var services = new ServiceCollection();
            RegisterResolvers(services);

            registerResolverOverrides.ForEach(register => register(services));

            _serviceProvider = _serviceProvider 
                ?? services.BuildServiceProvider();
            return _serviceProvider;
        }

        private static void RegisterResolvers(IServiceCollection services)
        {
            services.Scan(sc => sc
                .FromCallingAssembly()
                .FromAssemblies(AutoResolvedAssemblies)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
        }
    }
}