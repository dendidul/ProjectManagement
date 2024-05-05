using Core.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Auth
{
    public class ValidateAccessTokenResult : Response
    
    {
        public AuthAccessTokenData AccessTokenData;
    }
    public class ValidateAccessTokenWithUrlResult : Response
    {
        public AuthAccessTokenData AccessTokenData;
        public AuthAccessTokenDataWithUrl AccessTokenDataWithUrl;
    }
}
