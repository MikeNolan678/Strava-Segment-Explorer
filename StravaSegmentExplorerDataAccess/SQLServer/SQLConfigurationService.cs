using Microsoft.Extensions.Configuration;
using System;

namespace StravaSegmentExplorerDataAccess.SQLServer
{
    public class SQLConfigurationService
    {
        public string GetConnectionString(IConfiguration configuration, string name)
        {
            //var builder = new ConfigurationBuilder();
            //builder.SetBasePath(Directory.GetCurrentDirectory());
            //builder.AddJsonFile("appsettings.json");
            //var configuration = builder.Build();

            return configuration.GetConnectionString(name);
        }
    }
}