namespace Ecocell.Api;

public static class Bootstrapper
{
    public static void AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        AddAuthIdentity(services, configuration);
    }

    private static void AddAuthIdentity(IServiceCollection services, IConfiguration configuration)
    {        
        services.AddScoped(option => new AuthApiIdentity(GetApiIdentity(configuration)));
    }

    private static string GetApiIdentity(IConfiguration configuration)
    {
        var apiIdentity = configuration.GetRequiredSection("Configurations:ApiIdentity").Value;

        return apiIdentity;    
    }    
}