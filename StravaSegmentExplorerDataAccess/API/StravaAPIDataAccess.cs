using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using StravaSegmentExplorerDataAccess.Models;
using StravaSegmentExplorerDataAccess.SQLServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace StravaSegmentExplorerDataAccess.API
{
    public class StravaAPIDataAccess
    {

        //public StravaAPIDataAccess(IConfiguration configuration)
        //{
        //    configuration.GetSection("ConnectionStrings").Bind(_sqlConnectionConfig);

        //    _identityDbConnection = _sqlConnectionConfig.IdentityDbConnection;
        //    _stravaDbConnection = _sqlConnectionConfig.StravaDbConnection;

        //    configuration.GetSection("OAuthSettings").Bind(_sqlConnectionConfig);
        //    _clientId = _sqlConnectionConfig.ClientId;
        //    _clientSecret = _sqlConnectionConfig.ClientSecret;

        //}

        private readonly string _identityDbConnection;
        private readonly string _stravaDbConnection;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public StravaAPIDataAccess(IConfiguration configuration)
        {
           var configSettings = SQLConfigurationService.GetConfigurationSettings(configuration);

            _identityDbConnection = configSettings.IdentityDbConnection;
            _stravaDbConnection = configSettings.StravaDbConnection;
            _clientId = configSettings.ClientId;
            _clientSecret = configSettings.ClientSecret;
        }

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://www.strava.com/oauth"),
        };

        public async Task<bool> ConnectToStravaApi(HttpRequest request, string userID)
        {
            string error = request.Query["error"];
            string scope = request.Query["scope"];
            string code = request.Query["code"];

            if (error == "access_denied" || scope != "read,activity:read_all")
            {
                //handle authorisation request denied / incorrect scope
                //Could redirect to authorization page, with message to say request was denied/wrong scope. 
                //Button on page to re-authorize with strava

                throw new Exception("Request denied or incorrect scope granted.");
            }

            try
            {
                AccessTokenModel accessToken = await GetStravaToken(code);

                SQLOperations sqlOperations = new SQLOperations(); 

                sqlOperations.SaveAccessTokenAndAthleteData(accessToken, userID,_stravaDbConnection, _identityDbConnection);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AuthorizeWithStrava(string encodedRedirectUri)
        {
            var clientId = _clientId;

            return $"https://www.strava.com/oauth/authorize?client_id={clientId}&response_type=code&approval_prompt=auto&redirect_uri={encodedRedirectUri}&scope=activity:read_all";

        }

        private async Task<AccessTokenModel> GetStravaToken(string code)
        {
            string requestUri = $"{httpClient.BaseAddress}/token";

            var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("grant_type", "authorization_code")
                };

            var content = new FormUrlEncodedContent(postData);

            var response = await httpClient.PostAsync(requestUri, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var accessToken = JsonSerializer.Deserialize<AccessTokenModel>(result);

                return accessToken;
            }
            else
            {
                throw new Exception();
            }
        }


        //Need to implement refresh logic. Token expires 6 hours after it is issued.
        private async Task<AccessTokenModel> GetStravaRefreshToken(string code)
        {
            string requestUri = $"{httpClient.BaseAddress}/token";

            var postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("client_secret", _clientSecret),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("refresh_token", code)
                };

            var content = new FormUrlEncodedContent(postData);

            var response = await httpClient.PostAsync(requestUri, content);


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var parsedResult = JsonSerializer.Deserialize<AccessTokenModel>(result);

                return parsedResult;
            }
            else
            {
                throw new Exception();
            }
        }



    }
}

