using System.Security.Cryptography;
using System.Text;

namespace DealUp.Utils;

public static class CryptoUtils
{
    public static string GetRandomString(int length = 64, bool isLowercase = false)
    {
        return RandomNumberGenerator.GetHexString(length, isLowercase);
    }

    public static byte[] ToBytes(this string input)
    {
        return Encoding.ASCII.GetBytes(input);
    }
    
    public static string ToSha256(this string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(inputBytes);
        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        
        return hashString;
    }
}