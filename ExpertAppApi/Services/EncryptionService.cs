using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ExpertAppApi.Utilities;

public class EncryptionService(int saltSize, int keySize, int iterations, char delimiter) : IEncryptionService
{
    private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA256;
    
    public string Encrypt(string input)
    {
        var salt = RandomNumberGenerator.GetBytes(saltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, AlgorithmName, keySize);
        return String.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Verify(string hashedInput, string input)
    {
        string[] el = hashedInput.Split(delimiter);
        byte[] salt = Convert.FromBase64String(el[0]);
        byte[] hash = Convert.FromBase64String(el[1]);

        byte[] hashInput = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, AlgorithmName, keySize);
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}