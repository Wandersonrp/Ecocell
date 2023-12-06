using Dapper;
using Microsoft.Data.SqlClient;

namespace Ecocell.Infrastructure.Migrations;

public static class Database
{
    public static void CreateDatabase(string connectionString, string databaseName)
    {
        using var connection = new SqlConnection(connectionString);

        var parameters = new DynamicParameters();
        parameters.Add("name", databaseName);

        var records = connection.Query("SELECT database_id FROM sys.databases WHERE Name = @name", parameters);

        if (!records.Any()) connection.Execute($"CREATE DATABASE {databaseName}");
    }
}