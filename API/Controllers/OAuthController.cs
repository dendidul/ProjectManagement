using Application.Repositories.UserApi;
using Application.Wrapper.Auth;
using Core.Dto.Auth;
using Core.Dto.Client;
using Core.Dto.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenTracing;
using OpenTracing.Tag;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {

        private  ITracer _tracer;
        private  IAuthAccessToken _authAccessToken;
        private IUserAPIDA _userAPIDA;

        public OAuthController(ITracer tracer, IAuthAccessToken authAccessToken, IUserAPIDA userAPIDA)
        {
            _userAPIDA = userAPIDA;
            _tracer = tracer;
            _authAccessToken = authAccessToken  ;
        }

        [ProducesResponseType(typeof(ResponseData<AccessTokenResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseData<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseData<object>), StatusCodes.Status404NotFound)]
        [HttpPost]
        [Route("token")]
        public ResponseData<AccessTokenResponse> Post([FromHeader] string Authorization, [FromHeader] string RequestTime)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            var url = controllerName + "\\" + actionName;
            using var scope = _tracer.BuildSpan(url).StartActive(true);
            string traceId = _tracer.ActiveSpan.Context.TraceId;

            var logs = new LogAPI()
            {
                RequestHeader = "\"Authorized\" : " + Authorization + "\"RequestTime\" : " + RequestTime,
                ExecuteDate = DateTime.Now,
                MethodBaseUrl = url
            };


          
            

            // var check = _userAPIDA.GetData("ddc0e3f1-83ce-44eb-81ab-08a6028528f3", "48b45c30-c985-4f47-a953-0f2f44b5f133");

            try
            {
                var response = new ResponseData<AccessTokenResponse>()
                {
                    Status = new Status()
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Type = "SUCCESS",
                        Message = "Success",
                        MessageInd = "Sukses",
                        Errors = new List<Error>(),
                        
                    }
                };
                response.TraceId = traceId;

                var request = "\"Authorized\" : " + Authorization + "\"RequestTime\" : " + RequestTime;
                var clientKey = string.Empty;
                var clientSecret = string.Empty;

                if (Authorization != null && Authorization.StartsWith("Basic "))
                {
                    var encodedClientBase64 = Authorization.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    var decodedClientIdSecret = Encoding.UTF8.GetString(Convert.FromBase64String(encodedClientBase64));

                    clientKey = decodedClientIdSecret.Split(':', 2)[0];
                    clientSecret = decodedClientIdSecret.Split(':', 2)[1];
                  
                }

                var relativeUrl = "/api/oauth/token";

              

                if ((string.IsNullOrEmpty(clientKey) && string.IsNullOrEmpty(clientSecret)) || string.IsNullOrEmpty(RequestTime))
                {
                    response = new ResponseData<AccessTokenResponse>()
                    {
                        Status = StatusResponse.FailedInvalidClientIDSecret,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;   

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //_logging.LogWarning("{@data}", _logData.Log("Warning.OAuth.Token", JsonConvert.SerializeObject(response.Status)));
                    //_logApi.Insert(SetLogAPI(logApi, false, request, JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));

                    return response;
                }

               
                var getAccess = _userAPIDA.AuthorizeUrlByAPIKey(clientKey, "/api/oauth/token");

                var getClientConfigurationData = new ClientCardWithUrlLimit();
                
                if(getAccess != null)
                {
                    getClientConfigurationData.IsAccessActive = true;
                    getClientConfigurationData.IsActive = true;
                    getClientConfigurationData.ExpiredDate = DateTime.Now.AddYears(1);
                }

               

                if (getClientConfigurationData == null)
                {
                    response = new ResponseData<AccessTokenResponse>()
                    {
                        Status = StatusResponse.FailedClientIDSecretNotFound,
                        TimeStamp = DateTime.Now
                    };

                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //_logging.LogWarning("{@data}", _logData.Log("Warning.OAuth.Token", JsonConvert.SerializeObject(response.Status)));
                    //_logApi.Insert(SetLogAPI(logApi, false, request, JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;

                }

                if (!getClientConfigurationData.IsActive)
                {
                    response = new ResponseData<AccessTokenResponse>()
                    {
                        Status = StatusResponse.FailedInvalidClientIDSecret,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;

                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //_logging.LogWarning("{@data}", _logData.Log("Warning.OAuth.Token", JsonConvert.SerializeObject(response.Status)));
                    //_logApi.Insert(SetLogAPI(logApi, false, request, JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }

                if (getClientConfigurationData.ExpiredDate < DateTime.Now)
                {
                    response = new ResponseData<AccessTokenResponse>()
                    {
                        Status = StatusResponse.FailedClientIDSecretExpired,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;

                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //_logging.LogWarning("{@data}", _logData.Log("Warning.OAuth.Token", JsonConvert.SerializeObject(response.Status)));
                    //_logApi.Insert(SetLogAPI(logApi, false, request, JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }

                if (getClientConfigurationData != null && !getClientConfigurationData.IsAccessActive)
                {
                    response = new ResponseData<AccessTokenResponse>()
                    {
                        Status = StatusResponse.FailedAuthenticationNotPermissionMethod,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;

                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //_logging.LogWarning("{@data}", _logData.Log("Warning.OAuth.Token", JsonConvert.SerializeObject(response.Status)));
                    //_logApi.Insert(SetLogAPI(logApi, false, request, JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }

                response = _authAccessToken.GetAccessToken(clientKey, clientSecret, Convert.ToInt64(RequestTime));

                if (response.Status.Code == "200")
                {
                    //_logging.LogInformation("{@data}", _logData.Log("Success.OAuth.Token", JsonConvert.SerializeObject(response)));
                    //_logApi.Insert(SetLogAPI(logApi, true, request, JsonConvert.SerializeObject(response)));
                }
                else
                {
                    //_logging.LogWarning("{@data}", _logData.Log("Warning.OAuth.Token", JsonConvert.SerializeObject(response.Status)));
                    //_logApi.Insert(SetLogAPI(logApi, false, request, JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                }


                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;

               
                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));


                return response;
            }
            catch (Exception ex)
            {

                var response = new ResponseData<AccessTokenResponse>()
                {
                    Status = new Status()
                    {
                        Code = StatusCodes.Status400BadRequest.ToString(),
                        Type = "ERROR",
                        Message = "Failed",
                        MessageInd = "Gagal",
                        Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Message = ex.Message,
                                Type = "ERROR"
                            }
                        }
                    },
                    TimeStamp = DateTime.Now
                };
                response.TraceId = traceId;
                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;

                scope.Span.SetTag(Tags.Error, true);
                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));



                return response;
            }

        }
    }
}
