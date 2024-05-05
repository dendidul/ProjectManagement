using Application.Repositories.UserApi;
using Core.Dto.Auth;
using Core.Dto.Client;
using Core.Dto.Common;
using Infrastructure.Helper.Auth;
using Infrastructure.Helper.Config;
using Infrastructure.Helper.Encryption;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper.Auth
{
    public class ValidateAccessToken : IValidateAccessToken
    {

        private IConfigCreatorHelper _config;
        private IAccessTokenHelper _accessToken;
        private IStringEncryption _stringEncryption;
        private ISignature _signature;
        private readonly int _lifeTimeToken;
        private IUserAPIDA _userAPIDA;


        public ValidateAccessToken(IConfigCreatorHelper config, IAccessTokenHelper accessToken, IStringEncryption stringEncryption, ISignature signature
            
            ,IUserAPIDA userAPIDA
            )
        {

            _config = config;
            _accessToken = accessToken;
            _stringEncryption = stringEncryption;
            _signature = signature;
            _userAPIDA = userAPIDA;
            _lifeTimeToken = _config.GetInteger("AccessToken:Token.Cache.Lifetime.Minute.Integer");
        }

        public ValidateAccessTokenResult Validate(string accessToken)
        {
            var accessTokenDecrypt = _stringEncryption.Decrypt(accessToken);

            string key = Encoding.UTF8.GetString(Convert.FromBase64String(accessTokenDecrypt));
            string[] parts = key.Split(new char[] { ':' });

            if (parts.Length != 5)
                return getAuthenticationFailureResponse();

            string hash = parts[0];
            string clientID = parts[1];
            string clientApiKey = parts[2];
            string clientApiSecret = parts[3];
            long ticks = long.Parse(parts[4]);

            //var redisName = string.Format(_config.Get("AccessToken:RedisName.Format.String"), parts[4]);

            //var clientConfig = _cache.Get(redisName);
            //if (string.IsNullOrEmpty(clientConfig))
            //{
            //    return new ValidateAccessTokenResult()
            //    {
            //        Status = new Status()
            //        {
            //            Code = StatusCodes.Status400BadRequest.ToString(),
            //            Type = "ERROR",
            //            Message = "Finish with errors",
            //            MessageInd = "Finish with errors",
            //            Errors = new List<Error>() { new Error() { Message = "Get redis name failure. "+ redisName, MessageInd = "Gagal get redis name. " + redisName, Type = "ERROR" } }
            //        }
            //    };
            //}

            var timeStamp = new DateTime(ticks);

            //bool expired = Math.Abs((DateTime.Now - timeStamp).TotalSeconds) > _lifeTimeToken;

            bool expired = DateTime.Now > timeStamp;

            if (expired)
            {
                return new ValidateAccessTokenResult()
                {
                    Status = new Core.Dto.Common.Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Finish with errors",
                        MessageInd = "Finish with errors",
                        Errors = new List<Error>() { new Error() { Message = "Token Expired.", MessageInd = "Token Expired.", Type = "ERROR" } }
                    }
                };
            }

            //var configCache = JsonConvert.DeserializeObject<AuthAccessTokenData>(clientConfig);
            // ini gue comment dulu
            //var clientConfigurationList = _clientDA.ClientListWithSecretKey();

            //var getClientConfigurationData = new ClientCardWithSecretKey();

            //var getClientConfigurationData = clientConfigurationList.Where(x => x.ClientApiKey == clientApiKey).FirstOrDefault();

            var getAuthorized = _userAPIDA.GetUserApi(clientApiKey);

            var getClientConfigurationData = new ClientCardWithUrlLimit();

            if (getAuthorized != null)
            {
                getClientConfigurationData.IsAccessActive = true;
                getClientConfigurationData.IsActive = true;
                getClientConfigurationData.ExpiredDate = DateTime.Now.AddYears(1);
                getClientConfigurationData.ClientApiKey = getAuthorized.api_key;
                getClientConfigurationData.ClientApiSecret = getAuthorized.secret_key;
            }

            if (getClientConfigurationData == null)
            {
                return new ValidateAccessTokenResult()
                {
                    Status = new Core.Dto.Common.Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Finish with errors",
                        MessageInd = "Finish with errors",
                        Errors = new List<Error>() { new Error() { Message = "Client Configuration not found.", MessageInd = "Client Configuration tidak ditemukan.", Type = "ERROR" } }
                    }
                };
            }

            bool apiKeyExpired = getClientConfigurationData.ExpiredDate < DateTime.Now;
            if (apiKeyExpired)
            {
                return new ValidateAccessTokenResult()
                {
                    Status = new Core.Dto.Common.Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Finish with errors",
                        MessageInd = "Finish with errors",
                        Errors = new List<Error>() { new Error() { Message = "Token Expired.", MessageInd = "Token Expired.", Type = "ERROR" } }
                    }
                };
            }
            //var clientData = _validateGlobalClient.ValidateClient(clientApiKey, applicationChannel, string.Empty, 0);

            var clientSecretForToken = _signature.GenerateSHA256String(string.Join(":", clientApiKey, getClientConfigurationData.ClientApiSecret, ticks));

            if (clientApiKey != getClientConfigurationData.ClientApiKey || clientApiSecret != clientSecretForToken)
            {
                return new ValidateAccessTokenResult()
                {
                    Status = new Core.Dto.Common.Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Finish with errors",
                        MessageInd = "Finish with errors",
                        Errors = new List<Error>() { new Error() { Message = string.Format("ApiKey: {0}; configApiKey: {1}; apiSecret: {2}; GenerateSecret: {3}; configApiSecret: {4}", clientApiKey, getClientConfigurationData.ClientApiKey, clientApiSecret, clientSecretForToken, getClientConfigurationData.ClientApiSecret), MessageInd = "", Type = "ERROR" } }
                    }
                };
            }

            var accessTokenData = new AuthAccessTokenData()
            {
                ClientConfigurationID = getClientConfigurationData.ClientConfigurationID,
                ClientCode = getClientConfigurationData.ClientCode,
                ClientApiKey = getClientConfigurationData.ClientApiKey,
                ClientApiSecret = getClientConfigurationData.ClientApiSecret,
                ClientID = getClientConfigurationData.ClientId,
                ExpiredDate = getClientConfigurationData.ExpiredDate

            };

            return getAuthenticationSuccessResponse(accessTokenData);
        }

        public ValidateAccessTokenWithUrlResult ValidateWithUrl(string accessToken, string relativeUrl)
        {
            var accessTokenDecrypt = _stringEncryption.Decrypt(accessToken);

            string key = Encoding.UTF8.GetString(Convert.FromBase64String(accessTokenDecrypt));
            string[] parts = key.Split(new char[] { ':' });

            if (parts.Length != 5)
                return getAuthenticationWithUrlFailureResponse();

            string hash = parts[0];
            string clientID = parts[1];
            string clientApiKey = parts[2];
            string clientApiSecret = parts[3];
            long ticks = long.Parse(parts[4]);

            //var redisName = string.Format(_config.Get("AccessToken:RedisName.Format.String"), parts[4]);

            //var clientConfig = _cache.Get(redisName);
            //if (string.IsNullOrEmpty(clientConfig))
            //{
            //    return new ValidateAccessTokenResult()
            //    {
            //        Status = new Status()
            //        {
            //            Code = StatusCodes.Status400BadRequest.ToString(),
            //            Type = "ERROR",
            //            Message = "Finish with errors",
            //            MessageInd = "Finish with errors",
            //            Errors = new List<Error>() { new Error() { Message = "Get redis name failure. "+ redisName, MessageInd = "Gagal get redis name. " + redisName, Type = "ERROR" } }
            //        }
            //    };
            //}

            //var configCache = JsonConvert.DeserializeObject<AuthAccessTokenData>(clientConfig);
            //var clientConfigurationList = _clientDA.ClientListWithSecretKey();


            //var getClientConfigurationData = clientConfigurationList.Where(x => x.ClientApiKey == clientApiKey).FirstOrDefault();
            var getAuthorized = _userAPIDA.AuthorizeUrlByAPIKey(clientApiKey, relativeUrl);

            var getClientConfigurationData = new ClientCardWithUrlLimit();

            if(getAuthorized != null)
            {
                getClientConfigurationData.IsAccessActive = true;
                getClientConfigurationData.IsActive = true;
                getClientConfigurationData.ExpiredDate = DateTime.Now.AddYears(1);
                getClientConfigurationData.ClientApiKey = getAuthorized.api_key;
                getClientConfigurationData.ClientApiSecret = getAuthorized.secret_key;
            }



            if (getClientConfigurationData == null)
            {
                return new ValidateAccessTokenWithUrlResult()
                {
                    Status = new Core.Dto.Common.Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Finish with errors",
                        MessageInd = "Finish with errors",
                        Errors = new List<Error>() { new Error() { Message = "Client Configuration not found.", MessageInd = "Client Configuration tidak ditemukan.", Type = "ERROR" } }
                    }
                };
            }

            if (getClientConfigurationData.IsExpiredToken)
            {
                var timeStamp = new DateTime(ticks);

                //bool expired = Math.Abs((DateTime.Now - timeStamp).TotalSeconds) > _lifeTimeToken;
                bool expired = DateTime.Now > timeStamp;

                if (expired)
                {
                    return new ValidateAccessTokenWithUrlResult()
                    {
                        Status = new Core.Dto.Common.Status()
                        {
                            Code = StatusCodes.Status400BadRequest.ToString(),
                            Type = "ERROR",
                            Message = "Finish with errors",
                            MessageInd = "Finish with errors",
                            Errors = new List<Error>() { new Error() { Message = "Token Expired.", MessageInd = "Token Expired.", Type = "ERROR" } }
                        }
                    };
                }
            }


            bool apiKeyExpired = getClientConfigurationData.ExpiredDate < DateTime.Now;
            if (apiKeyExpired)
            {
                return new ValidateAccessTokenWithUrlResult()
                {
                    Status = new Core.Dto.Common.Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Finish with errors",
                        MessageInd = "Finish with errors",
                        Errors = new List<Error>() { new Error() { Message = "Token Expired.", MessageInd = "Token Expired.", Type = "ERROR" } }
                    }
                };
            }
            //var clientData = _validateGlobalClient.ValidateClient(clientApiKey, applicationChannel, string.Empty, 0);

            var clientSecretForToken = _signature.GenerateSHA256String(string.Join(":", clientApiKey, getClientConfigurationData.ClientApiSecret, ticks));

            //if (clientApiKey != getClientConfigurationData.ClientApiKey || clientApiSecret != clientSecretForToken)
            //{
            //    return new ValidateAccessTokenWithUrlResult()
            //    {
            //        Status = new Status()
            //        {
            //            Code = StatusCodes.Status400BadRequest.ToString(),
            //            Type = "ERROR",
            //            Message = "Finish with errors",
            //            MessageInd = "Finish with errors",
            //            Errors = new List<Error>() { new Error() { Message = "Invalid client id and client secret.", MessageInd = "Client id dan client secret tidak valid.", Type = "ERROR" } }
            //        }
            //    };
            //}

            var accessTokenDataWithUrl = new AuthAccessTokenDataWithUrl()
            {
                ClientConfigurationID = getClientConfigurationData.ClientConfigurationID,
                ClientCode = getClientConfigurationData.ClientCode,
                ClientApiKey = getClientConfigurationData.ClientApiKey,
                ClientApiSecret = getClientConfigurationData.ClientApiSecret,
                ClientID = getClientConfigurationData.ClientId,
                ExpiredDate = getClientConfigurationData.ExpiredDate,
                LimitHit = getClientConfigurationData.LimitHit,
                LimitBlast = getClientConfigurationData.LimitBlast,
                LimitBlastOtp = getClientConfigurationData.LimitBlastOtp,
                BaseUrl = getClientConfigurationData.BaseUrl,
                IsByPassLimit = getClientConfigurationData.IsByPassLimit,
                IsAccessActive = getClientConfigurationData.IsAccessActive,
                MethodID = getClientConfigurationData.MethodID,
                ApplicationKeyID = getClientConfigurationData.ApplicationKeyID
            };

            var accessTokenData = new AuthAccessTokenData()
            {
                ClientConfigurationID = getClientConfigurationData.ClientConfigurationID,
                ClientCode = getClientConfigurationData.ClientCode,
                ClientApiKey = getClientConfigurationData.ClientApiKey,
                ClientApiSecret = getClientConfigurationData.ClientApiSecret,
                ClientID = getClientConfigurationData.ClientId,
                ExpiredDate = getClientConfigurationData.ExpiredDate
            };

            return getAuthenticationWithUrlSuccessResponse(accessTokenData, accessTokenDataWithUrl);
        }

        #region private method
        //private void refreshToken(string redisName, AuthAccessTokenData configCache)
        //{
        //    configCache.ExpiredDate = DateTime.Now.AddSeconds(_lifeTimeToken);

        //    _cache.Set(redisName, JsonConvert.SerializeObject(configCache), _lifeTimeToken);
        //}

        private ValidateAccessTokenResult getAuthenticationFailureResponse()
        {
            return new ValidateAccessTokenResult()
            {
                Status = StatusResponse.FailedAuthentication
            };
        }

        private ValidateAccessTokenWithUrlResult getAuthenticationWithUrlFailureResponse()
        {
            return new ValidateAccessTokenWithUrlResult()
            {
                Status = StatusResponse.FailedAuthentication
            };
        }

        private ValidateAccessTokenWithUrlResult getAuthenticationWithUrlSuccessResponse(AuthAccessTokenData authAccessToken, AuthAccessTokenDataWithUrl authAccessTokenWithUrl)
        {
            return new ValidateAccessTokenWithUrlResult()
            {
                AccessTokenData = authAccessToken,
                AccessTokenDataWithUrl = authAccessTokenWithUrl,
                Status = new Core.Dto.Common.Status()
                {
                    Code = StatusCodes.Status200OK.ToString(),
                    Type = "SUCCESS",
                    Message = "Success",
                    MessageInd = "Sukses",
                    Errors = new List<Error>()
                }
            };
        }

        private ValidateAccessTokenResult getAuthenticationSuccessResponse(AuthAccessTokenData authAccessToken)
        {
            return new ValidateAccessTokenResult()
            {
                AccessTokenData = authAccessToken,
                Status = new Core.Dto.Common.Status()
                {
                    Code = StatusCodes.Status200OK.ToString(),
                    Type = "SUCCESS",
                    Message = "Success",
                    MessageInd = "Sukses",
                    Errors = new List<Error>()
                }
            };
        }
        #endregion
    }
}
