using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DL.Data.Infrastructure.ContextControl
{
    public class DotnetMigrationsContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, "..", "ClientLayer/");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetValue<string>("DatabaseConnectionString");
            return new ApplicationDbContext(connectionString);
        }
    }
}