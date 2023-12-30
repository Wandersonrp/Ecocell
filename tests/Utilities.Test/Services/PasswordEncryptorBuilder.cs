using Ecocell.Application.Services.Criptography;

namespace Utilities.Test.Services;

public class PasswordEncryportBuilder
{
    public static PasswordEncryptor Instance()
    {
        return new PasswordEncryptor("ACVSKSBH-KSKNA");
    }
}