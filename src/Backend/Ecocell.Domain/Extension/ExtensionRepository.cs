using Microsoft.Extensions.Configuration;

namespace Ecocell.Domain.Extension;

public static class ExtensionRepository
{
    public static string GetDatabaseName(this IConfiguration configuration)
    {        
        var databaseName = configuration.GetConnectionString("DatabaseName");

        return databaseName;
    }

    public static string GetConnectionString(this IConfiguration configuration)
    {        
        var connectionString = configuration.GetConnectionString("Connection");

        return connectionString;
    }

    public static string GetFullConnection(this IConfiguration configuration)
    {
        var databaseName = configuration.GetDatabaseName();
        var connectionString = configuration.GetConnectionString();

        return $"{connectionString}Database={databaseName}";
    }
}