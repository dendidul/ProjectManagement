using Infrastructure.Helper.Config;
using Infrastructure.Helper.Encryption;
//using DMQI.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Cache;
using Core.Dto.Encryption;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Core.Dto.Common;
using Core.Dto.Auth;

namespace Infrastructure.WebServices
{
    public class KalbeConnectAPIService : IKalbeConnectAPIService
    {
        private string _apiPath = string.Empty;
        private string _headerType;
        private string _clientApiSecret;
        private string _clientApiKey;
        private string _kalbeConnectRedisToken;
        private IConfigCreatorHelper _config;
        private ISignature _signature;
        private IRedisCache _cache;

        public KalbeConnectAPIService(IConfigCreatorHelper config,
            ISignature signature, 
            IRedisCache cache)
        {
            _config = config;
            _signature = signature;
            _cache = cache;
            _apiPath = _config.Get("External:API:NewKalbeConnect:URL.String");
            _clientApiSecret = _config.Get("External:API:NewKalbeConnect:ClientApiSecret.String");
            _clientApiKey = _config.Get("External:API:NewKalbeConnect:ClientApiKey.String");
            _headerType = RequestHeaderType.APPLICATION_X_WWW_FORM_URLENCODED;
            _kalbeConnectRedisToken = "NewKalbeConnect:FPRSBlastLinkAccessTokenKalbe";
        }

        public struct RequestHeaderType
        {
            public const string APPLICATION_X_WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";
            public const string APPLICATION_JSON = "application/json";
            public const string TEXT_HTML = "text/html";
            public const string TEXT_XML = "text/xml";
        }

        public string GetToken()
        {
            try
            {
                int lifeTime = 0;

                //string accessToken = _cache.Get(_kalbeConnectRedisToken);
                string accessToken = _cache.Get(_kalbeConnectRedisToken);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    return accessToken;
                }

                var requestTimeTick = DateTime.Now.Ticks;

                var clientSecret = _signature.GenerateSHA256String(string.Join(":", _clientApiKey, _clientApiSecret, requestTimeTick));

                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(_clientApiKey + ":" + clientSecret));

                var relativeUri = "/oauth/token";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);

                    client.DefaultRequestHeaders.Add("RequestTime", requestTimeTick.ToString());

                    string uri = relativeUri.IndexOf('/') == 0 ? String.Join("", _apiPath, relativeUri) : String.Join("/", _apiPath, relativeUri);

                    var response = client.PostAsync(uri, null).Result;

                    var result = JsonConvert.DeserializeObject<ResponseData<AccessTokenResponse>>(response.Content.ReadAsStringAsync().Result);

                    accessToken = result.Data.AccessToken;
                    long tickExpired = result.Data.Expired;

                    lifeTime = Convert.ToInt32((new DateTime(tickExpired).Subtract(DateTime.Now).TotalSeconds) / 2); // di bagi 2 karna kadang kena token expired

                    _cache.Set(_kalbeConnectRedisToken, accessToken, lifeTime);
                }

                return accessToken;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public HttpResponseMessage PostJsonAsyncWithToken<T>(string relativeUri, T obj, bool useSignature = false)
        {
            //HttpClient client = null;
            HttpResponseMessage response = null;
            var content = "{}";
            var time = DateTime.Now.Ticks.ToString();

            try
            {
                if (obj != null)
                {
                    content = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                }

                var token = GetToken();

                var sign = string.Empty;

                if (useSignature)
                {
                    var requestBody = Regex.Replace(content, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");

                    sign = _signature.GetSignature(Signature(token, "POST", "/api" + relativeUri, requestBody, time), _clientApiSecret);
                }

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_headerType));
                    client.DefaultRequestHeaders.Add("AccessToken", token);
                    client.DefaultRequestHeaders.Add("DateTimeTick", time);
                    client.DefaultRequestHeaders.Add("Signature", sign);

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}{1}", _apiPath, relativeUri));
                    request.Content = new StringContent(content, Encoding.UTF8, "application/json");
                    response = client.SendAsync(request).Result;

                    //var result = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                //if (client != null) client.Dispose();
                throw ex;
            }

            return response;
        }

        public HttpResponseMessage GetWithToken(string relativeUri, bool useSignature = false)
        {
            //HttpClient client = null;
            HttpResponseMessage response = null;

            var time = DateTime.Now.Ticks.ToString();

            try
            {
                var token = GetToken();
                var sign = string.Empty;

                if (useSignature)
                {
                    sign = _signature.GetSignature(Signature(token, "GET", "/api" + relativeUri, string.Empty, time), _clientApiSecret);
                }

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(_headerType));
                    client.DefaultRequestHeaders.Add("AccessToken", token);
                    client.DefaultRequestHeaders.Add("DateTimeTick", time);
                    client.DefaultRequestHeaders.Add("Signature", sign);

                    string uri = relativeUri.IndexOf('/') == 0 ? String.Join("", _apiPath, relativeUri) : String.Join("/", _apiPath, relativeUri);
                    response = client.GetAsync(uri).Result;
                }
            }
            catch (Exception)
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
            //finally
            //{
            //    if (client != null) client.Dispose();
            //}

            return response;
        }







        #region private

        private SignatureRequest Signature(string accessToken, string methode, string relativeUri, string requestBody, string time)
        {
            return new SignatureRequest()
            {
                AccessToken = accessToken,
                HttpMethod = methode,
                RelativeUrl = relativeUri,
                RequestBody = requestBody,
                DateTimeTick = time
            };
        }

        #endregion
    }
}
