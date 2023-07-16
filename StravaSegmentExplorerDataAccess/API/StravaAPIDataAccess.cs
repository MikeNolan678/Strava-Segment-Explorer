using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using StravaSegmentExplorerDataAccess.Models;
using StravaSegmentExplorerDataAccess.SQLServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
        private readonly string _identityDbConnection;
        private readonly string _stravaDbConnection;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _userId;
        private readonly IHttpContextAccessor _httpContextAccessor;

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
            BaseAddress = new Uri("https://www.strava.com")
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

                return false;
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
            string requestUri = $"{httpClient.BaseAddress}oauth/token";

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

        public async Task<List<ActivityModel>> GetListOfActivities(string _userId)
        {
            AppUserModel appUser;
            //Not implemented, but here for future use
            //string before = "";
            //string after = "";

            int page = 1;
            string perPage = "200";

            try
            {
                SQLOperations sqlOperations = new SQLOperations();
                appUser = sqlOperations.GetCurrentUserData(_userId, _stravaDbConnection);
            }
            catch (Exception)
            {
                throw new Exception("Could not get User Data");
            }

            try
            {
                List<ActivityModel> activityList = new List<ActivityModel>();
                List<ActivityModel> resultActivityModel = new List<ActivityModel>();

                do
                {
                    string requestUri = $"{httpClient.BaseAddress}api/v3/athlete/activities?page={page}&per_page={perPage}";

                    var authorizationHeaderValue = $"Bearer {appUser.AccessToken}";

                    if (httpClient.DefaultRequestHeaders.Contains("Authorization"))
                    {
                        httpClient.DefaultRequestHeaders.Remove("Authorization");
                    }

                    httpClient.DefaultRequestHeaders.Add("Authorization", authorizationHeaderValue);

                    var response = await httpClient.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        resultActivityModel = JsonSerializer.Deserialize<List<ActivityModel>>(result);

                        activityList.AddRange(resultActivityModel);
                        page++;

                        //Console.WriteLine(page);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                while (resultActivityModel.Count >= 1);

                return activityList;
            }
            catch (Exception e)
            {
                throw new Exception($"{e.Message}, {e.StackTrace}");
            }
        }

    }
}

