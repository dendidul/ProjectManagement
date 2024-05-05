using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Client
{
    public class ClientCardResponse
    {
        public int ClientConfigurationID { get; set; }
        public string ClientCode { get; set; }
        public string ClientApiKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    public class ClientCard : ClientCardResponse
    {
        public int ClientId { get; set; }
    }

    public class ClientCardWithSecretKey : ClientCard
    {
        public string ClientApiSecret { get; set; }
    }

    public class ClientCardWithUrlLimit : ClientCardWithSecretKey
    {
        public int MethodID { get; set; }
        public int ApplicationKeyID { get; set; }
        public string BaseUrl { get; set; }
        public int? LimitHit { get; set; }
        public int? LimitBlast { get; set; }
        public int? LimitBlastOtp { get; set; }
        public bool IsByPassLimit { get; set; }
        public bool IsAccessActive { get; set; }
        public bool IsExpiredToken { get; set; }
    }
}
