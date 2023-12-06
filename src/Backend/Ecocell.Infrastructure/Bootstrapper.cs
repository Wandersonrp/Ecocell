using System.Reflection;
using Ecocell.Domain.Extension;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecocell.Infrastructure;

public static class Bootstrapper
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager) 
    {    
        services.AddFluentMigratorCore().ConfigureRunner(config => 
            config.AddSqlServer()
            .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("Ecocell.Infrastructure")).For.All()
            );
    }
}