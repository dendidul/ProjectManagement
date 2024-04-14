using API.Utils;
using Application.Wrapper.Auth;
using Application.Wrapper.Utility;
using Core.Dto.Common;
using Core.Dto.Encryption;
using Infrastructure.Helper.Encryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenTracing;
using OpenTracing.Tag;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private IStringEncryption _stringEncryption;
        private IValidateAccessToken _validateAccessToken;
        private IPasswordEncryption _passwordEncryption;
        private ITracer _tracer;
        private IUtilityWrapper _utilityWrapper;

        public UtilityController(ITracer tracer, IPasswordEncryption passwordEncryption,IStringEncryption stringEncryption, IValidateAccessToken validateAccessToken, IUtilityWrapper utilityWrapper)
        {
            _passwordEncryption = passwordEncryption;
            _stringEncryption = stringEncryption;
            _validateAccessToken = validateAccessToken;
            _tracer = tracer;
            _utilityWrapper = utilityWrapper;   
        }


        [ProducesResponseType(typeof(ResponseData<EncryptionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseData<object>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ResponseData<EncryptionResponse> Encrypt([FromHeader] string SecretKey, [FromBody] EncryptionRequest request)
        {

            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            var url = controllerName + "\\" + actionName;
            using var scope = _tracer.BuildSpan(url).StartActive(true);
            string traceId = _tracer.ActiveSpan.Context.TraceId;

            var logs = new LogAPI()
            {
                RequestHeader = "\"SecretKey\" : " + SecretKey,
                RequestParam = JsonConvert.SerializeObject(request),
                ExecuteDate = DateTime.Now,
                MethodBaseUrl = url
            };

            try
            {
                var response = new ResponseData<EncryptionResponse>()
                {
                    Status = new Status()
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Type = "SUCCESS",
                        Message = "Success",
                        MessageInd = "Sukses",
                        Errors = new List<Error>()
                    },
                    Data = new EncryptionResponse()
                };
                response.TraceId = traceId;
                var clientApiKey = string.Empty;

                var accessTokenDecrypt = _stringEncryption.Decrypt(request.AccessToken);

                string key = Encoding.UTF8.GetString(Convert.FromBase64String(accessTokenDecrypt));
                string[] parts = key.Split(new char[] { ':' });

                clientApiKey = parts[2];

                var relativeUrl = "/api/utility/encrypt";

               
                var validate = _validateAccessToken.ValidateWithUrl(request.AccessToken, relativeUrl);
             

                if (!validate.Status.Code.Equals("200"))
                {
                    response = new ResponseData<EncryptionResponse>()
                    {
                        Status = validate.Status,
                        TimeStamp = DateTime.Now
                    };

                    response.Status.Errors.Add(new Error() { Message = "Access Token doesn't have permission.", MessageInd = "Akses Token tidak mempunyai izin", Type = "ERROR" });
                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));


                    // _logApi.Insert(SetLogAPI(logApi, false, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }

                if (validate.AccessTokenDataWithUrl == null || (validate.AccessTokenDataWithUrl != null && !validate.AccessTokenDataWithUrl.IsAccessActive))
                {
                    response = new ResponseData<EncryptionResponse>()
                    {
                        Status = StatusResponse.FailedAuthenticationNotPermissionMethod,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //response.Status.Errors.Add(new Error() { Message = "Access Token doesn't have permission.", MessageInd = "Akses Token tidak mempunyai izin", Type = "ERROR" });
                    //  _logApi.Insert(SetLogAPI(logApi, false, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }
                var errorList = new List<Error>();

               
                if (string.IsNullOrEmpty(request.Password))
                {
                    errorList.Add(new Error() { Type = "Error", Message = "Password not found.", MessageInd = "Password tidak ditemukan." });

                    response = new ResponseData<EncryptionResponse>()
                    {
                        Status = new Status()
                        {
                            Code = StatusCodes.Status400BadRequest.ToString(),
                            Type = "ERROR",
                            Message = "Finish with errors",
                            MessageInd = "Finish with errors",
                            Errors = errorList
                        },
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //response.Status.Errors.Add(new Error() { Message = "Access Token doesn't have permission.", MessageInd = "Akses Token tidak mempunyai izin", Type = "ERROR" });
                    //  _logApi.Insert(SetLogAPI(logApi, false, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }

                response.Data.EncryptedPassword = _passwordEncryption.Encrypt(request.Password);

                //  _logApi.Insert(SetLogAPI(logApi, true, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response)));

                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;             
                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));


                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseData<EncryptionResponse>()
                {
                    Status = StatusResponse.FailedException,
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new Error() { Message = ex.Message, Type = "ERROR" });
                response.TraceId = traceId;
                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;

                scope.Span.SetTag(Tags.Error, true);
                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                return response;
            }
        }

        [ProducesResponseType(typeof(ResponseData<DecryptionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseData<object>), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ResponseData<DecryptionResponse> Decrypt([FromHeader] string SecretKey, [FromBody] DecryptionRequest request)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            var url = controllerName + "\\" + actionName;
            using var scope = _tracer.BuildSpan(url).StartActive(true);
            string traceId = _tracer.ActiveSpan.Context.TraceId;

            var logs = new LogAPI()
            {
                RequestHeader = "\"SecretKey\" : " + SecretKey,
                RequestParam = JsonConvert.SerializeObject(request),
                ExecuteDate = DateTime.Now,
                MethodBaseUrl = url
            };

            try
            {
                var response = new ResponseData<DecryptionResponse>()
                {
                    Status = new Status()
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Type = "SUCCESS",
                        Message = "Success",
                        MessageInd = "Sukses",
                        Errors = new List<Error>()
                    },
                    Data = new DecryptionResponse()
                };
                response.TraceId = traceId;
                var clientApiKey = string.Empty;

                var accessTokenDecrypt = _stringEncryption.Decrypt(request.AccessToken);

                string key = Encoding.UTF8.GetString(Convert.FromBase64String(accessTokenDecrypt));
                string[] parts = key.Split(new char[] { ':' });

                clientApiKey = parts[2];

               
                var relativeUrl = "/api/utility/decrypt";
                var validate = _validateAccessToken.ValidateWithUrl(request.AccessToken, relativeUrl);
                //var logApi = new LogAPIWithDesc()
                //{
                //    MethodBaseUrl = relativeUrl,
                //    ApplicationApiKey = clientApiKey,
                //    ResponseParam = JsonConvert.SerializeObject(request),
                //    ExecuteDate = DateTime.Now
                //};

                if (!validate.Status.Code.Equals("200"))
                {
                    response = new ResponseData<DecryptionResponse>()
                    {
                        Status = validate.Status,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    //response.Status.Errors.Add(new Error() { Message = "Access Token doesn't have permission.", MessageInd = "Akses Token tidak mempunyai izin", Type = "ERROR" });
                    //  _logApi.Insert(SetLogAPI(logApi, false, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }

                if (validate.AccessTokenDataWithUrl == null || (validate.AccessTokenDataWithUrl != null && !validate.AccessTokenDataWithUrl.IsAccessActive))
                {
                    response = new ResponseData<DecryptionResponse>()
                    {
                        Status = StatusResponse.FailedAuthenticationNotPermissionMethod,
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;
                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));
                    //response.Status.Errors.Add(new Error() { Message = "Access Token doesn't have permission.", MessageInd = "Akses Token tidak mempunyai izin", Type = "ERROR" });
                    //  _logApi.Insert(SetLogAPI(logApi, false, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));
                    return response;
                }
                var errorList = new List<Error>();


                if (string.IsNullOrEmpty(request.EncryptedPassword))
                {
                    errorList.Add(new Error() { Type = "Error", Message = "Password not found.", MessageInd = "Password tidak ditemukan." });

                    response = new ResponseData<DecryptionResponse>()
                    {
                        Status = new Status()
                        {
                            Code = StatusCodes.Status400BadRequest.ToString(),
                            Type = "ERROR",
                            Message = "Finish with errors",
                            MessageInd = "Finish with errors",
                            Errors = errorList
                        },
                        TimeStamp = DateTime.Now
                    };
                    response.TraceId = traceId;

                    response.Status.Errors.Add(new Error() { Message = "Access Token doesn't have permission.", MessageInd = "Akses Token tidak mempunyai izin", Type = "ERROR" });
                    // _logApi.Insert(SetLogAPI(logApi, false, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response), string.Join("," + string.Empty, response.Status.Errors.Select(x => x.Message).ToArray())));

                    logs.ResponseParam = JsonConvert.SerializeObject(response);
                    logs.ResponseDate = DateTime.Now;

                    scope.Span.SetTag(Tags.Error, true);
                    scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                    return response;
                }

                response.Data.Password = _passwordEncryption.Decrypt(request.EncryptedPassword);

                //_logApi.Insert(SetLogAPI(logApi, true, JsonConvert.SerializeObject(request), JsonConvert.SerializeObject(response)));

                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;

                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));


                return response;
            }
            catch (Exception ex)
            {
                var response = new ResponseData<DecryptionResponse>()
                {
                    Status = StatusResponse.FailedException,
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new Error() { Message = ex.Message, Type = "ERROR" });
                response.TraceId = traceId;

                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;

                scope.Span.SetTag(Tags.Error, true);
                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                return response;
            }
        }


        [ProducesResponseType(typeof(ResponseData<EncryptParamUrlResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseData<object>), StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(AuthorizedAccessTokenAttribute))]
        [HttpPost]
        public ResponseData<EncryptParamUrlResponse> EncryptParamUrl(EncryptParamUrlRequest request)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            var url = controllerName + "\\" + actionName;
            using var scope = _tracer.BuildSpan(url).StartActive(true);
            string traceId = _tracer.ActiveSpan.Context.TraceId;

            var accessToken = HttpContext.Request.Headers["AccessToken"].ToString();

            var logs = new LogAPI()
            {
                RequestHeader = "\"Access Token\" : " + accessToken,
                RequestParam = JsonConvert.SerializeObject(request),
                ExecuteDate = DateTime.Now,
                MethodBaseUrl = url
            };

            try
            {
                var response = new ResponseData<EncryptParamUrlResponse>()
                {
                    Status = new Status()
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Type = "SUCCESS",
                        Message = "Success",
                        MessageInd = "Sukses",
                        Errors = new List<Error>()
                    },
                    Data = new EncryptParamUrlResponse()
                };
                response.TraceId = traceId;
                response = _utilityWrapper.EncryptParamUrl(request);


                return response;
            }
            catch (Exception ex)
            {

                var response = new ResponseData<EncryptParamUrlResponse>()
                {
                    Status = StatusResponse.FailedException,
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new Error() { Message = ex.Message, Type = "ERROR" });
                response.TraceId = traceId;

                logs.ResponseParam = JsonConvert.SerializeObject(response);
                logs.ResponseDate = DateTime.Now;

                scope.Span.SetTag(Tags.Error, true);
                scope.Span.Log(DateTime.Now, JsonConvert.SerializeObject(logs));

                return response;
            }
        }


        [ProducesResponseType(typeof(ResponseData<DecryptParamUrlResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseData<object>), StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(AuthorizedAccessTokenAttribute))]
        [HttpPost]
        public ResponseData<DecryptParamUrlResponse> DecryptParamUrl(DecryptParamUrlRequest request)
        {
            string actionName = ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString();

            var url = controllerName + "\\" + actionName;
            using var scope = _tracer.BuildSpan(url).StartActive(true);
            string traceId = _tracer.ActiveSpan.Context.TraceId;

            var accessToken = HttpContext.Request.Headers["AccessToken"].ToString();

            var logs = new LogAPI()
            {
                RequestHeader = "\"Access Token\" : " + accessToken,
                RequestParam = JsonConvert.SerializeObject(request),
                ExecuteDate = DateTime.Now,
                MethodBaseUrl = url
            };

            try
            {
                var response = new ResponseData<DecryptParamUrlResponse>()
                {
                    Status = new Status()
                    {
                        Code = StatusCodes.Status200OK.ToString(),
                        Type = "SUCCESS",
                        Message = "Success",
                        MessageInd = "Sukses",
                        Errors = new List<Error>()
                    },
                    Data = new DecryptParamUrlResponse()
                };
                response.TraceId = traceId;
                response = _utilityWrapper.DecryptParamUrl(request);


                return response;
            }
            catch (Exception ex)
            {

                var response = new ResponseData<DecryptParamUrlResponse>()
                {
                    Status = StatusResponse.FailedException,
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new Error() { Message = ex.Message, Type = "ERROR" });
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
