using Core.Dto.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Encryption
{
    public class Signature : ISignature
    {
        public Signature()
        {

        }

        public string GetSignature(SignatureRequest request, string apiSecretKey)
        {
            string result = "";
            try
            {
                var requestBody = request.RequestBody.ToLower();
                string requestBodyEncode = GenerateSHA256String(Regex.Replace(requestBody, @"\s+", ""));
                string stringToSign = string.Format("{0}:{1}:{2}:{3}:{4}", request.HttpMethod, request.RelativeUrl.ToLower(), request.AccessToken, requestBodyEncode, request.DateTimeTick);
                result = CalculateHMACSHA256(apiSecretKey, stringToSign);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return result;
        }

        public string CalculateHMACSHA256(string apiSecretKey, string stringToSign)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(apiSecretKey);

            using (HMAC hmac = new HMACSHA256(keyByte))
            {
                byte[] messageBytes = encoding.GetBytes(stringToSign);
                byte[] hash = hmac.ComputeHash(messageBytes);
                return GetLowerStringFromHash(hash, false);
            }
        }

        public string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetLowerStringFromHash(hash, false);
        }

        

        public string GetLowerStringFromHash(byte[] hash, bool upperCase)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString(upperCase ? "X2" : "x2"));
            }
            return result.ToString().ToLower();
        }

        public string GenerateBase64Encode(string inputString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            return Convert.ToBase64String(bytes);
        }
    }
}
