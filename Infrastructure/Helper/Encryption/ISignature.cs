using Core.Dto.Encryption;

namespace Infrastructure.Helper.Encryption
{
    public interface ISignature
    {
        string GetSignature(SignatureRequest request, string apiSecretKey);

        string CalculateHMACSHA256(string apiSecretKey, string stringToSign);

        string GenerateSHA256String(string inputString);
        string GetLowerStringFromHash(byte[] hash, bool upperCase);

        string GenerateBase64Encode(string inputString);

     }
}