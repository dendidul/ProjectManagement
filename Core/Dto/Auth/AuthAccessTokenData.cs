using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Auth
{
    public class AuthAccessTokenData
    {
        public int ClientConfigurationID { get; set; }
        public int ClientID { get; set; }
        public string ClientCode { get; set; }
        public string ClientApiKey { get; set; }
        public string ClientApiSecret { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    public class AuthAccessTokenDataWithUrl : AuthAccessTokenData
    {
        public int MethodID { get; set; }
        public int ApplicationKeyID { get; set; }
        public string BaseUrl { get; set; }
        public int? LimitHit { get; set; }
        public int? LimitBlast { get; set; }
        public int? LimitBlastOtp { get; set; }
        public bool IsByPassLimit { get; set; }
        public bool IsAccessActive { get; set; }
    }

    public class CardNumberPrefix
    {
        public string CardPrefix { get; set; }
        public string UseFor { get; set; }
    }
}
