using System.Security.Cryptography;
using System.Text;

namespace Ecocell.Application.Services.Criptography;

public class PasswordEncryptor
{
    private readonly string _encryptorKey;

    public PasswordEncryptor(string encryptorKey)
    {
        _encryptorKey = encryptorKey;
    }

    public string Encrypt(string password)
    {
        var passwordWithSalt = $"{password}{_encryptorKey}";

        var bytes = Encoding.UTF8.GetBytes(passwordWithSalt);
        var sha512 = SHA512.Create();
        byte[] hashBytes = sha512.ComputeHash(bytes);
        var passwordHashed = StringBytes(hashBytes);

        return passwordHashed;
    }

    private static string StringBytes(byte[] bytes) 
    {
        var sb = new StringBuilder();

        foreach(byte b in bytes) 
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}