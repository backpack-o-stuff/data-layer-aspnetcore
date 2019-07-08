using DL.Application.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DL.ClientLayer.Infrastructure
{
    public class SettingsProvider : ISettingsProvider
    {
        private readonly IConfiguration _configuration;

        public SettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DatabaseConnectionString()
        {
            return _configuration.GetSection("DatabaseConnectionString").Value;
        }
    }
}