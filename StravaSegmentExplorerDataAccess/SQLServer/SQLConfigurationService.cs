using Microsoft.Extensions.Configuration;
using StravaSegmentExplorerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.SQLServer
{
    public static class SQLConfigurationService
    {
        private static readonly StravaConnectionConfigModel _connectionString = new StravaConnectionConfigModel();
        
        public static SQLConnectionConfigModel GetConfigurationSettings(IConfiguration configuration)
        {
            SQLConnectionConfigModel sqlConnectionConfig = new SQLConnectionConfigModel();

            configuration.GetSection("ConnectionStrings").Bind(_connectionString);
            sqlConnectionConfig.IdentityDbConnection = _connectionString.IdentityDbConnection;
            sqlConnectionConfig.StravaDbConnection = _connectionString.StravaDbConnection;

            configuration.GetSection("OAuthSettings").Bind(_connectionString);
            sqlConnectionConfig.ClientId = _connectionString.ClientId;
            sqlConnectionConfig.ClientSecret = _connectionString.ClientSecret;

            return sqlConnectionConfig;
        }
    }
}
