using Ecocell.Application.Services.Token;

namespace Utilities.Test.Services;

public class TokenControllerBuilder
{
    public static TokenController Instance()
    {
        return new TokenController(10, "Ykx2Tm1jNW9INFFEd1c3NmxxZEJUS3d2cVQyaHhsWU1UYQ==");
    }
}