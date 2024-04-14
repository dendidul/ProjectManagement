using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebServices
{
    public interface ILoyaltyAPIService
    {
        string GetToken();
        HttpResponseMessage PostJsonAsyncWithToken<T>(string relativeUri, T obj, bool useSignature = false);
        HttpResponseMessage GetWithToken(string relativeUri, bool useSignature = false);
    }
}
