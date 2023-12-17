using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper.Configuration.Annotations;
using Microsoft.IdentityModel.Tokens;

namespace Ecocell.Application.Services.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private readonly double _tokenLifeTimeInMinutes;
    private readonly string _securityKey;

    public TokenController(double tokenLifeTimeInMinutes, string securityKey)
    {
        _tokenLifeTimeInMinutes = tokenLifeTimeInMinutes;
        _securityKey = securityKey;
    }   

    public string GenerateToken(string userEmail)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, userEmail),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenLifeTimeInMinutes),
            SigningCredentials = new SigningCredentials(SymmetricKey(),SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }

    public void ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SymmetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    private SymmetricSecurityKey SymmetricKey()
    {
        var symetricKeyBase64 = Convert.FromBase64String(_securityKey);
        var symetricKey = new SymmetricSecurityKey(symetricKeyBase64);

        return symetricKey;
    }
}