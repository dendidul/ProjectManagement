using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.LoyaltyAPI
{
    public class LoyaltyAccessTokenResponse
    {
        public string AccessToken { get; set; }
        public long Expired { get; set; }
    }
}
