using Core.Dto.Auth;
using Core.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Auth
{
    public interface IAuthAccessToken
    {
        ResponseData<AccessTokenResponse> GetAccessToken(string clientApiKey, string clientApiSecret, long requestTime);
    }
}
