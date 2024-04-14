using Core.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Auth
{
    public interface IValidateAccessToken
    {
        ValidateAccessTokenResult Validate(string accessToken);
        ValidateAccessTokenWithUrlResult ValidateWithUrl(string accessToken, string relativeUrl);
    }
}
