using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using StravaSegmentExplorerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.SQLServer
{
    public class SQLOperations
    {
        public void SaveAccessTokenAndAthleteData(AccessTokenModel accessToken, string userID, string _stravaDbConnection, string _identityDbConnection)
        {
            string mergeSqlAccessToken = @"
            MERGE INTO dbo.AccessToken AS Target 
            USING (SELECT @AthleteId, @Id, @AccessToken, @ExpiresAt) AS Source (AthleteID, ID, AccessToken, ExpiresAt) 
            ON Target.AthleteID = Source.AthleteID AND Target.ID = Source.ID
            WHEN MATCHED THEN 
                UPDATE SET AccessToken = Source.AccessToken, ExpiresAt = Source.ExpiresAt
            WHEN NOT MATCHED BY TARGET THEN 
                INSERT (AthleteID, ID, AccessToken, ExpiresAt) 
                VALUES (Source.AthleteID, Source.ID, Source.AccessToken, Source.ExpiresAt);
        ";

            var parametersAccessToken = new
            {
                AthleteId = accessToken.Athlete.AthleteId,
                Id = userID,
                AccessToken = accessToken.AccessToken,
                ExpiresAt = accessToken.ExpiresAt,
            };

            SQLDataAccess.SaveData(mergeSqlAccessToken, parametersAccessToken, _stravaDbConnection);

            string mergeSqlAthlete = @"
            MERGE INTO dbo.Athlete AS Target 
            USING (SELECT @AthleteId, @Id, @FirstName, @LastName, @City, @State, @Country, @Sex, @Weight) AS Source 
            (AthleteId, Id, FirstName, LastName, City, State, Country, Sex, Weight) 
            ON Target.AthleteId = Source.AthleteId AND Target.Id = Source.Id
            WHEN MATCHED THEN 
                UPDATE SET 
                    FirstName = Source.FirstName, 
                    LastName = Source.LastName, 
                    City = Source.City, 
                    State = Source.State, 
                    Country = Source.Country,
                    Sex = Source.Sex, 
                    Weight = Source.Weight
            WHEN NOT MATCHED BY TARGET THEN 
                INSERT (AthleteId, Id, FirstName, LastName, City, State, Country, Sex, Weight) 
                VALUES (Source.AthleteId, Source.Id, Source.FirstName, Source.LastName, Source.City, Source.State, Source.Country, Source.Sex, Source.Weight);
        ";

            var parametersAthlete = new
            {
                AthleteId = accessToken.Athlete.AthleteId,
                Id = userID,
                FirstName = accessToken.Athlete.FirstName,
                LastName = accessToken.Athlete.LastName,
                City = accessToken.Athlete.City,
                State = accessToken.Athlete.State,
                Country = accessToken.Athlete.Country,
                Sex = accessToken.Athlete.Sex,
                Weight = accessToken.Athlete.Weight
            };

            SQLDataAccess.SaveData(mergeSqlAthlete, parametersAthlete, _stravaDbConnection);

            string usersTableSqlUpdate = "UPDATE dbo.AspNetUsers " +
                                          "SET IsStravaConnected = 1 " +
                                          "WHERE Id = @Id";

            var parametersUserTableUpdate = new
            {
                Id = userID
            };

            SQLDataAccess.SaveData(usersTableSqlUpdate, parametersUserTableUpdate, _identityDbConnection);

        }

        public AppUserModel GetCurrentUserData(string userID, string _stravaDbConnection)
        {
            string sql = @"
        SELECT 
            Athlete.AthleteId, 
            Athlete.FirstName, 
            Athlete.LastName, 
            Athlete.City, 
            Athlete.State, 
            Athlete.Country, 
            Athlete.Sex, 
            Athlete.Weight, 
            AccessToken.ExpiresAt, 
            AccessToken.AccessToken
        FROM 
            dbo.Athlete
        INNER JOIN 
            dbo.AccessToken ON Athlete.AthleteId = AccessToken.AthleteId
        WHERE 
            Athlete.Id = @Id
    ";

            var parameters = new
            {
                Id =  userID
            };

            return SQLDataAccess.LoadRecord<AppUserModel, dynamic>(sql, parameters, _stravaDbConnection).First();
        }

        //public bool IsConnectedToStrava(string userID)
        //{
        //    string sql = @"
        //SELECT 
        //    IsStravaConnected
        //FROM 
        //    dbo.AspNetUsers
        //WHERE 
        //    Id = @Id";

        //    var parameters = new
        //    {
        //        Id = userID
        //    };

        //    var result = SQLDataAccess.LoadRecord<bool?, dynamic>(sql, parameters, ).FirstOrDefault();

        //    return result ?? false;
        //}

    }
}

