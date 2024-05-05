using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper.Auth
{
    public  class AccessTokenHelper: IAccessTokenHelper
    {

        private readonly string _alg;

        public AccessTokenHelper()
        {
            _alg = "HmacSHA256";
        }

        public string GetToken(string clientID, string clientApiKey, string clientApiSecret, long ticks = 0)
        {
            var token = generateToken(clientID, clientApiKey, clientApiSecret, ticks);

            return token;
        }


        #region private method
        private string generateToken(string clientID, string clientApiKey, string clientApiSecret, long ticks = 0)
        {
            string clientIP = string.Empty;
            string hash = string.Join(":", new string[] { clientIP, clientApiKey, string.Empty, ticks.ToString() });
            string hashLeft = string.Empty;
            string hashRight = string.Empty;

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(clientApiSecret);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { clientID, clientApiKey, clientApiSecret, ticks.ToString() });
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }
        #endregion
    }

}
