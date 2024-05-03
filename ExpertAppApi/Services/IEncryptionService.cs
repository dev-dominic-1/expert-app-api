namespace ExpertAppApi.Utilities;

public interface IEncryptionService
{
    string Encrypt(string input);

    bool Verify(string hashedInput, string input);
}