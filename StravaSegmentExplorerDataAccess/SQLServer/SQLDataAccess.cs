using Dapper;
using Microsoft.Data.SqlClient;
using StravaSegmentExplorerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSegmentExplorerDataAccess.SQLServer
{
    public static class SQLDataAccess
    {
        public static IEnumerable<T> LoadRecord<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<T> record = connection.Query<T>(sqlStatement, parameters);

                return record;
            }
        }

        public static void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters);
            }
        }
    }
}
