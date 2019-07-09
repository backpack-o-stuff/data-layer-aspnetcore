using DL.Application.Infrastructure;

namespace DL.Data.Infrastructure.ContextControl
{
    public interface IDbContextFactory
    {
        ApplicationDbContext For();
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

        public ApplicationDbContext For()
        {
            var connectionString = _settingsProvider.DatabaseConnectionString();
            return new ApplicationDbContext(connectionString);
        }
    }
}