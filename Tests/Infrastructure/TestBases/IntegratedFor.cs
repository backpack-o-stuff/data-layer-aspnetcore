using System;
using System.Collections.Generic;
using DL.ClientLayer.Infrastructure.IoC;
using DL.Data.EF.Infrastructure;
using DL.Tests.Infrastructure.Fakes;
using Microsoft.Extensions.DependencyInjection;

namespace DL.Tests.Infrastructure.TestBases
{
    public class IntegratedFor<T> : ArrangeActAssertOn
        where T : class
    {
        protected readonly List<Action<IServiceCollection>> DependencyFakes = new List<Action<IServiceCollection>>();

        protected T SUT;

        public IntegratedFor()
        {
            SharedBeforeAll();
            SUT = Resolve<T>();
        }

        protected TResolveFor Resolve<TResolveFor>()
        {
            return DependencyRegistrations.Resolve<TResolveFor>(DependencyFakes);
        }

        private void SharedBeforeAll()
        {
            DependencyFakes.Add((services) => 
            {
                services.AddScoped<IDbContextFactory, EntityFrameworkInMemoryDbContextFactory>();
            });
        }
    }
}