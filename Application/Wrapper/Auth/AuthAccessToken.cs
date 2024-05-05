using Application.Repositories.UserApi;
using Core.Dto.Auth;
using Core.Dto.Common;
using Core.Dto.Global;
using Infrastructure.Helper.Auth;
using Infrastructure.Helper.Encryption;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Auth
{
    public class AuthAccessToken : IAuthAccessToken
    {
        private ISignature _signature;
        private IStringEncryption _stringEncryption;
        private IAccessTokenHelper _accessToken;
        private IUserAPIDA _userAPIDA;


        public AuthAccessToken(ISignature signature, IStringEncryption stringEncryption, IAccessTokenHelper accessToken, IUserAPIDA userAPIDA)
        {
            _signature = signature;
            _stringEncryption = stringEncryption;
            _accessToken = accessToken;
            _userAPIDA = userAPIDA;
        }

        private int _lifeTime = 900;

        #region private method
        private ResponseData<AccessTokenResponse> getResponseClientNotFound()
        {
            return new ResponseData<AccessTokenResponse>()
            {
                Status = StatusResponse.FailedClientIDSecretNotFound,
                TimeStamp = DateTime.Now
            };
        }

        private ResponseData<AccessTokenResponse> getResponseClientExpired()
        {
            return new ResponseData<AccessTokenResponse>()
            {
                Status = StatusResponse.FailedClientIDSecretExpired,
                TimeStamp = DateTime.Now
            };
        }

        private ResponseData<AccessTokenResponse> getResponseRequestTimeExpired()
        {
            return new ResponseData<AccessTokenResponse>()
            {
                Status = StatusResponse.FailedTokenRequestTimeExpired,
                TimeStamp = DateTime.Now
            };
        }

        private ResponseData<AccessTokenResponse> getResponseEncryptNotMatch()
        {
            return new ResponseData<AccessTokenResponse>()
            {
                Status = StatusResponse.FailedTokenEncryptNotMatch,
                TimeStamp = DateTime.Now
            };
        }

        private ResponseData<AccessTokenResponse> getAccessTokenResponse(long ticks, string token)
        {
            var response = new ResponseData<AccessTokenResponse>()
            {
                Status = new Core.Dto.Common.Status()
                {
                    Code = StatusCodes.Status200OK.ToString(),
                    Type = "SUCCESS",
                    Message = "Success",
                    MessageInd = "Sukses",
                    Errors = new List<Error>()
                },
                Data = new AccessTokenResponse()
                {
                    AccessToken = token,
                    Expired = ticks
                }
            };

            return response;
        }

        public ResponseData<AccessTokenResponse> GetAccessToken(string clientApiKey, string clientApiSecret, long requestTime)
        {
            ClientProfileConfiguration clientProfileConfiguration = new ClientProfileConfiguration();

            var getUserAPI = _userAPIDA.GetUserApi(clientApiKey);

            var config = new ClientConfiguration();



            clientProfileConfiguration.Name = getUserAPI.api_key_name;           
            config.ClientConfigurationID = 1;
            config.ClientApiKey = clientApiKey;          
            config.ClientApiSecret = getUserAPI.secret_key;
            clientProfileConfiguration.Configuration = config;

            // clientProfileConfiguration.exp

            var ticks = DateTime.Now.AddSeconds(_lifeTime).Ticks;

            //var ticks = 637890885150000000;

            var accessTokenData = getAccessTokenData(clientProfileConfiguration, ticks);

            var parsing = new DateTime(requestTime);

            if (parsing.AddMinutes(3) < DateTime.Now)
            {
                // Plus TimeZone JKT
                var totalMinuts = (parsing.AddHours(7).AddMinutes(3) - DateTime.Now).TotalMinutes;
                if (totalMinuts > 0 && totalMinuts < 5)
                    parsing = parsing.AddHours(7);
            }

            if (parsing.AddMinutes(3) < DateTime.Now)
                return getResponseRequestTimeExpired();

            var clientSecret = _signature.GenerateSHA256String(string.Join(":", clientApiKey, accessTokenData.ClientApiSecret, requestTime));

            if (clientSecret != clientApiSecret)
                return getResponseEncryptNotMatch();

            var clientSecretForToken = _signature.GenerateSHA256String(string.Join(":", clientApiKey, accessTokenData.ClientApiSecret, ticks));

            var token = _stringEncryption.Encrypt(_accessToken.GetToken(accessTokenData.ClientID.ToString(), clientApiKey, clientSecretForToken, ticks));

            //var redisName = string.Format(_config.Get("AccessToken:RedisName.Format.String"), ticks);

            ResponseData<AccessTokenResponse> response = getAccessTokenResponse(ticks, token);

            //_cache.Set(redisName, JsonConvert.SerializeObject(accessTokenData), _lifeTime);

            return response;


        }

        private AuthAccessTokenData getAccessTokenData(ClientProfileConfiguration clientProfileConfiguration, long ticks)
        {
            var response = new AuthAccessTokenData();

            var config = clientProfileConfiguration.Configuration;

            response.ClientConfigurationID = config.ClientConfigurationID;
            response.ClientApiKey = config.ClientApiKey;
            response.ClientID = clientProfileConfiguration.ClientID;
            response.ClientApiSecret = config.ClientApiSecret;
            response.ExpiredDate = new DateTime(ticks);

            return response;
        }
        #endregion

    }
}
