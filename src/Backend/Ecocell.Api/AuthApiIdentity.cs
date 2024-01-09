using Microsoft.AspNetCore.Mvc;

namespace Ecocell.Api;

public class AuthApiIdentity
{
    private readonly string _apiIdentity;

    public AuthApiIdentity(string apiIdentity)
    {
        _apiIdentity = apiIdentity;
    }

    public string GetApiIdentity()
    {
        return _apiIdentity;
    }

    public bool ReturnUnauthorizedMessage(string? token, out ObjectResult result)
    {
        result = null;
        var isValidToken = ValidateToken(token);

        if(!isValidToken) 
        {
            result = new ObjectResult(new
            {
                message = "Unauthorized",
                status = StatusCodes.Status401Unauthorized
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };            
        }     

        return isValidToken;   
    }

    public bool ValidateToken(string? token)
    {
        bool isValidToken = true;

        if(token == null)
        {
            isValidToken = false;
            return isValidToken;
        }

        if(token != null) 
        {
            if (_apiIdentity != token.Split(" ")[1]) 
            {
                isValidToken = false;
            }
        }
             

        return isValidToken;
    }
}