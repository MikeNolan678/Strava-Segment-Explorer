using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.Interfaces
{
    internal interface IConfirgurationSettingsProvider
    {
         
    }
}

/*
 * Refactor:

Create a separate class to retrieve configuration settings:

public class ConfigurationSettingsProvider : IConfigurationSettingsProvider
{
    private readonly string _identityDbConnection;
    private readonly string _stravaDbConnection;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public ConfigurationSettingsProvider(IConfiguration configuration)
    {
        var configSettings = SQLConfigurationService.GetConfigurationSettings(configuration);

        _identityDbConnection = configSettings.IdentityDbConnection;
        _stravaDbConnection = configSettings.StravaDbConnection;
        _clientId = configSettings.ClientId;
        _clientSecret = configSettings.ClientSecret;
    }

    public string IdentityDbConnection => _identityDbConnection;
    public string StravaDbConnection => _stravaDbConnection;
    public string ClientId => _clientId;
    public string ClientSecret => _clientSecret;
}

Modify your LeaderboardModel and other classes to use the new class:

public class LeaderboardModel : PageModel
{
    private readonly IConfigurationSettingsProvider _configSettingsProvider;

    public LeaderboardModel(IConfigurationSettingsProvider configSettingsProvider)
    {
        _configSettingsProvider = configSettingsProvider;
    }

    // Use _configSettingsProvider to access the configuration settings

    // ... additional code specific to LeaderboardModel ...
}

 */