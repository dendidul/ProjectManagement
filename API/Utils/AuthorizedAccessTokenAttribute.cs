using Application.Repositories.UserApi;
using Application.Wrapper.Auth;
using Core.Dto.Common;
using Infrastructure.Helper.Encryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace API.Utils
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizedAccessTokenAttribute : ActionFilterAttribute
    {
        private IValidateAccessToken _validateAccessToken;
        private readonly IConfiguration _configuration;
      
        private ILogger<AuthorizedAccessTokenAttribute> _logging;
     
        private IUserAPIDA _clientDA;
       
        private IStringEncryption _stringEncryption;

        public AuthorizedAccessTokenAttribute(IValidateAccessToken validateAccessToken, IConfiguration configuration, ILogger<AuthorizedAccessTokenAttribute> logging, IUserAPIDA clientDA, IStringEncryption stringEncryption)
        {
            _validateAccessToken = validateAccessToken;
            _configuration = configuration;
            _logging = logging;
            _clientDA = clientDA;
            _stringEncryption = stringEncryption;
        }


        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //var sessionID = Thread.GetNamedDataSlot("sessionID");
            //string machineName = new Regex("[^a-zA-Z0-9]").Replace(Environment.MachineName, string.Empty);
            //string guid = Guid.NewGuid().ToString("N");
            //string sessionId = string.Format("{0}{1}", machineName, guid);
            //Thread.SetData(sessionID, sessionId);

            var requestBodyJson = string.Empty;
            var requestBody = string.Empty;

            var method = actionContext.HttpContext.Request.Method;
            var relativeURL = actionContext.HttpContext.Request.Path.Value;

            if (method == "GET")
            {
                if (actionContext.ActionArguments.Count > 0)
                {
                    requestBodyJson = JsonConvert.SerializeObject(DictionaryToObject(actionContext.ActionArguments));
                    requestBody = Regex.Replace(requestBodyJson, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");
                }

                // relativeURL += actionContext.HttpContext.Request.QueryString;

                requestBody = "";
            }
            else
            {
                if (actionContext.ActionArguments.Count > 0)
                {
                    requestBodyJson = JsonConvert.SerializeObject(actionContext.ActionArguments["request"]);
                    requestBody = Regex.Replace(requestBodyJson, @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+", "$1");
                }
            }

            var apiRequest = new HeaderAPIRequest()
            {
                Header = actionContext.HttpContext.Request.Headers.Aggregate("", (current, header) => current + $"{header.Key}: {header.Value}{Environment.NewLine}"),
                AccessUrl = actionContext.HttpContext.Request.Path,
                Method = actionContext.HttpContext.Request.Method,
                Body = requestBody
            };

            var accessToken = actionContext.HttpContext.Request.Headers["AccessToken"].ToString();
            //var userToken = actionContext.HttpContext.Request.Headers["UserToken"].ToString();
            var timestamp = actionContext.HttpContext.Request.Headers["DateTimeTick"].ToString();
            var signature = actionContext.HttpContext.Request.Headers["Signature"].ToString();
          //  var applicationChannel = actionContext.HttpContext.Request.Headers["ApplicationChannel"].ToString();

           // actionContext.HttpContext.Items["ApplicationChannel"] = !string.IsNullOrEmpty(applicationChannel) ? applicationChannel : string.Empty;

            var clientApiKey = string.Empty;

            var success = true;

            //var logAPI = new LogAPIWithDesc()
            //{
            //    MethodBaseUrl = relativeURL,
            //    ApplicationApiKey = clientApiKey,
            //    RequestParam = requestBody,
            //    ExecuteDate = DateTime.Now,
            //    Status = false
            //};

            try
            {
                var accessTokenDecrypt = _stringEncryption.Decrypt(accessToken);

                string key = Encoding.UTF8.GetString(Convert.FromBase64String(accessTokenDecrypt));
                string[] parts = key.Split(new char[] { ':' });

                clientApiKey = parts[2];
            }
            catch
            {
                actionContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                actionContext.Result = getErrorMessage("999", "Invalid access token.", "Akses token tidak valid");

                //_logging.LogError("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute: Invalid access token", JsonConvert.SerializeObject(apiRequest)));
                //logAPI.ResponseParam = JsonConvert.SerializeObject(actionContext.Result);
                //logAPI.ResponseDate = DateTime.Now;
                //logAPI.ErrorMessage = "Invalid access token.";
                //logAPI.AccessToken = accessToken;
                //_logAPIDA.Insert(logAPI);
                success = false;

                return;
            }

            var validate = _validateAccessToken.ValidateWithUrl(accessToken, relativeURL);

            if (!validate.Status.Code.Equals("200"))
            {
                actionContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                actionContext.Result = getErrorMessage("999", "Invalid access token.", "Akses token tidak valid");

                //_logging.LogError("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute: Invalid access token", JsonConvert.SerializeObject(apiRequest)));
                //_logging.LogWarning("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute.ValidateToken", JsonConvert.SerializeObject(validate.Status)));
                //logAPI.ResponseParam = JsonConvert.SerializeObject(actionContext.Result);
                //logAPI.ResponseDate = DateTime.Now;
                //logAPI.ErrorMessage = "Invalid access token.";
                //logAPI.AccessToken = accessToken;
                //_logAPIDA.Insert(logAPI);
                success = false;

                return;
            }
            else
            {
                if (validate.AccessTokenDataWithUrl == null)
                {
                    actionContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    actionContext.Result = getErrorMessage("999", "Access Token doesn't have permission.", "Akses Token tidak mempunyai izin");

                    //_logging.LogError("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute: Access Token doesn't have permission", JsonConvert.SerializeObject(apiRequest)));
                    //_logging.LogWarning("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute.ValidateToken", JsonConvert.SerializeObject(validate.Status)));
                    //logAPI.ResponseParam = JsonConvert.SerializeObject(actionContext.Result);
                    //logAPI.ResponseDate = DateTime.Now;
                    //logAPI.ErrorMessage = "Access Token doesn't have permission.";
                    //logAPI.AccessToken = accessToken;
                    //_logAPIDA.Insert(logAPI);
                    success = false;

                    return;
                }

                if (validate.AccessTokenDataWithUrl != null && !validate.AccessTokenDataWithUrl.IsAccessActive)
                {
                    actionContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    actionContext.Result = getErrorMessage("999", "Access Token doesn't have permission.", "Akses Token tidak mempunyai izin");

                    //_logging.LogError("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute: Access Token doesn't have permission", JsonConvert.SerializeObject(apiRequest)));
                    //_logging.LogWarning("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute.ValidateToken", JsonConvert.SerializeObject(validate.Status)));
                    //logAPI.ResponseParam = JsonConvert.SerializeObject(actionContext.Result);
                    //logAPI.ResponseDate = DateTime.Now;
                    //logAPI.ErrorMessage = "Access Token doesn't have permission.";
                    //logAPI.AccessToken = accessToken;
                    //_logAPIDA.Insert(logAPI);
                    success = false;

                    return;
                }
                else
                {
                 //   var countLogSummaryAPI = 1;
                    //var getLogSummaryApi = _logSummaryAPIDA.GetByMethodApplicationKeyDate(validate.AccessTokenDataWithUrl.MethodID, validate.AccessTokenDataWithUrl.ApplicationKeyID, DateTime.Now);

                    //if (getLogSummaryApi != null)
                    //{
                    //    countLogSummaryAPI = getLogSummaryApi.Count + 1;
                    //}

                    //if (validate.AccessTokenDataWithUrl.LimitHit.HasValue && countLogSummaryAPI > validate.AccessTokenDataWithUrl.LimitHit.Value)
                    //{

                    //    actionContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    //    actionContext.Result = getErrorMessage("999", "Access Method over limit.", "Akses Method melebihi batas.");

                    //    //_logging.LogError("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute: Access Method over limit", JsonConvert.SerializeObject(apiRequest)));
                    //    //_logging.LogWarning("{@data}", _logData.Log("Error.AuthorizeTransactionTokenAttribute.summaryLog", JsonConvert.SerializeObject(validate.Status)));
                    //    //logAPI.ResponseParam = JsonConvert.SerializeObject(actionContext.Result);
                    //    //logAPI.ResponseDate = DateTime.Now;
                    //    //logAPI.ErrorMessage = "Access Method over limit.";
                    //    //logAPI.AccessToken = accessToken;
                    //    //_logAPIDA.Insert(logAPI);
                    //    success = false;

                    //    return;

                    //}


                    actionContext.HttpContext.Items["ClientConfiguration"] = validate.AccessTokenData;
                    actionContext.HttpContext.Items["ClientConfigurationWtihUrl"] = validate.AccessTokenDataWithUrl;
                    //actionContext.HttpContext.Items["LogSummaryAPI"] = new LogSummaryAPI()
                    //{
                    //    MethodID = validate.AccessTokenDataWithUrl.MethodID,
                    //    ApplicationKeyID = validate.AccessTokenDataWithUrl.ApplicationKeyID,
                    //    LogSummaryDate = DateTime.Now,
                    //    Count = countLogSummaryAPI
                    //};
                    actionContext.HttpContext.Items["ClientID"] = validate.AccessTokenData.ClientID;


                }

            }

            if (success)
            {
                //_logging.LogInformation("{@data}", _logData.Log("Success.AuthorizeTransactionTokenAttribute", JsonConvert.SerializeObject(apiRequest)));
                //var newLogAPI = new LogAPI()
                //{

                //    MethodID = validate.AccessTokenDataWithUrl.MethodID,
                //    ApplicationKeyID = validate.AccessTokenDataWithUrl.ApplicationKeyID,
                //    ExecuteDate = logAPI.ExecuteDate,
                //    RequestParam = requestBody
                //};
                //var logApiID = _logAPIDA.Insert(newLogAPI);
                //newLogAPI.LogID = logApiID;
                //actionContext.HttpContext.Items["LogAPI"] = newLogAPI;
            }

            base.OnActionExecuting(actionContext);
        }

        private JsonResult getErrorMessage(string type, string message, string messageInd)
        {
            return new JsonResult(new Response()
            {
                Status = new Status()
                {
                    Code = StatusCodes.Status401Unauthorized.ToString(),
                    Message = HttpStatusCode.Unauthorized.ToString(),
                    Errors = new System.Collections.Generic.List<Error>() {
                        new Error()
                        {
                            Type = type,
                            Message = message,
                            MessageInd = messageInd
                        }
                    }
                },
                TimeStamp = DateTime.Now
            });


        }

        private dynamic DictionaryToObject(IDictionary<String, Object> dictionary)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<String, Object>>)expandoObj;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }





    }
}
