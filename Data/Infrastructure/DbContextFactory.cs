using DL.Application.Infrastructure;

namespace DL.Data.Infrastructure
{
    public interface IDbContextFactory
    {
        ApplicationContext For();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly ISettingsProvider _settingsProvider;

        public DbContextFactory(
            ISettingsProvider settingsProvider
        )
        {
            _settingsProvider = settingsProvider;
        }

        public ApplicationContext For()
        {
            var connectionString = _settingsProvider.DatabaseConnectionString();
            return new ApplicationContext(connectionString);
        }
    }
}