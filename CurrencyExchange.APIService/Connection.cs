using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CurrencyExchange.APIService
{
    public class Connection
    {
        public IDbConnection InitializeDb()
        {

            //SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
            //var Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Config");
            ////Azure SQL Server Name 
            //conn.DataSource = Config["DataSource"];
            ////User to connect to Azure
            //conn.UserID = Config["UserID"];
            ////Password used in Azure
            //conn.Password = Config["Password"];
            ////Azure database name
            //conn.InitialCatalog = Config["InitialCatalog"];

            //Connect local SQL data

            var Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings");
            string connectionString = Config["SqlConnection"];
            SqlConnection conn = new SqlConnection(connectionString);
            var connection = new SqlConnection(conn.ConnectionString);
            connection.Open();

            // Migrate up
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            var migrationResourceNames = assembly.GetManifestResourceNames()
                .Where(x => x.EndsWith(".sql"))
                .OrderBy(x => x);
            if (!migrationResourceNames.Any()) throw new System.Exception("No migration files found!");
            foreach (var resourceName in migrationResourceNames)
            {
                var sql = GetResourceText(assembly, resourceName);
                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }

            return connection;
        }

        string GetResourceText(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
