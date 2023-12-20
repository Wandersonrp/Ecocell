using System.Reflection;
using Ecocell.Domain.Extension;
using Ecocell.Domain.Repository;
using Ecocell.Domain.Repository.UserApp;
using Ecocell.Domain.Repository.WorkUnity;
using Ecocell.Infrastructure.Context;
using Ecocell.Infrastructure.Repository.UserApp;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Ecocell.Domain.Repository.EletronicMaterial;
using Ecocell.Infrastructure.Context.Repository.EletronicMaterial;

namespace Ecocell.Infrastructure;

public static class Bootstrapper
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddFluentMigrator(services, configurationManager);
        AddRepository(services);
        AddWorkUnity(services);
        AddContext(services, configurationManager);
    }

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configurationManager) 
    {    
        services.AddFluentMigratorCore().ConfigureRunner(config => 
            config.AddSqlServer()
            .WithGlobalConnectionString(configurationManager.GetFullConnection()).ScanIn(Assembly.Load("Ecocell.Infrastructure")).For.All()
            );
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>()
            .AddScoped<IUserWriteOnlyRepository, UserRepository>();

        services.AddScoped<IEletronicMaterialWriteOnlyRepository, EletronicMaterialRepository>()
            .AddScoped<IUserWriteOnlyRepository, UserRepository>();   
    }

    private static void AddWorkUnity(IServiceCollection services)
    {
        services.AddScoped<IWorkUnity, WorkUnity>();
    }

    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {
        var connectionString = configurationManager.GetFullConnection();

        services.AddDbContext<EcocellContext>(contextOptions => 
            contextOptions.UseSqlServer(connectionString)
        );
    }
}