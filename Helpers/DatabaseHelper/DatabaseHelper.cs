using AutomationFramework.Constants;
using Dapper;
using MySql.Data.MySqlClient;


namespace AutomationFramework.Helpers.DatabaseHelper
{
    internal static class DatabaseHelper
    {
        
        public static async Task<IEnumerable<T>> ReadAllData<T>(string sport, string tableName,bool overrideQuery=false, string? database = null)
        {
            try
            {
                await using var connection = new MySqlConnection(GenerateDbUrl(sport, database));
                connection.Open();
                
                var query = overrideQuery ? tableName : $"SELECT * FROM {tableName}";
                var value = await connection.QueryAsync<T>(query);

                await connection.CloseAsync();
                return value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public static async Task InsertData<T>(string sport, string tableName, T dataObject, string? database = null)
        {
            try
            {
                await using var connection = new MySqlConnection(GenerateDbUrl(sport, database));
                connection.Open();

                var columns = string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
                var values = string.Join(", ", typeof(T).GetProperties().Select(p => $"@{p.Name}"));

                var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                query = query.Replace(", Key,", ", `Key`,", StringComparison.CurrentCulture);
                query = query.Replace(", Order,", ", `Order`,", StringComparison.CurrentCulture);
                await connection.ExecuteAsync(query, dataObject);
                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }

        public static async Task DeleteData(string sport, string tableName, string? database = null)
        {
            try
            {
                await using var connection = new MySqlConnection(GenerateDbUrl(sport,database));
                connection.Open();

                var query = $"DELETE FROM {tableName}";
                await connection.ExecuteAsync(query);
                await connection.CloseAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private static string GenerateDbUrl(string sport, string? database)
        {
            var databaseName = database == null ? KeywordMapping.GetSportMapping(sport) : database;
            var connectionString = "Server=" + sport.ToLower() + "-" +Base.Base.Configuration.DatabaseUrl +
                                   ";Database=" + databaseName + ";User Id=" + Base.Base.Configuration.DatabaseUsername + ";Password=" +
                                   Base.Base.Configuration.DatabasePassword + ";";
            return connectionString;
        }
    }
}
