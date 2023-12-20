using Ecocell.Application.Services.Criptography;
using Ecocell.Application.Services.Token;
using Ecocell.Application.UseCases.EletronicMaterial.Register;
using Ecocell.Application.UseCases.UserApp.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecocell.Application;

public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddPasswordEncryptor(services, configuration);
        AddTokenController(services, configuration);
        AddRepository(services);
    }

    private static void AddPasswordEncryptor(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(option => new PasswordEncryptor(GetSalt(configuration)));
    }

    private static void AddTokenController(IServiceCollection services, IConfiguration configuration)
    {
        (double TokenLifeTime, string SecurityKey) = GetTokenProperties(configuration);

        services.AddScoped(option => new TokenController(TokenLifeTime, SecurityKey));
    }

    private static string GetSalt(IConfiguration configuration) 
    {
        var section = configuration.GetRequiredSection("Configurations:Salt");

        var salt = section.Value;

        return salt;        
    }

    private static (int, string) GetTokenProperties(IConfiguration configuration)    
    {
        var configurationsSection = configuration.GetSection("Configurations");
        var tokenLifeTimeInMinutes = configurationsSection.GetRequiredSection("TokenLifeTimeInMinutes");
        var securityKey = configurationsSection.GetRequiredSection("SecurityKey");

        return (int.Parse(tokenLifeTimeInMinutes.Value), securityKey.Value);
    }

    private static void AddRepository(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IRegisterEletronicMaterialUseCase, RegisterEletronicMaterialUseCase>();
    }
}