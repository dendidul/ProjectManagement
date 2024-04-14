namespace Infrastructure.Helper.Encryption
{
    public interface IPasswordEncryption
    {
        string Encrypt(string password);
        string Decrypt(string cipherText);

    }
}