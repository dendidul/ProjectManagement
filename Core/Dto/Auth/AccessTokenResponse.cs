using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Auth
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }
        public long Expired { get; set; }
    }
}
