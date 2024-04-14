using Core.Dto.Common;
using Core.Dto.ExternalAPI.NewKalbeconnect;
using Infrastructure.Helper.Config;
using Infrastructure.WebServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ExternalAPI.NewKalbeConnect
{
    public class NewKalbeConnectAPI : INewKalbeConnectAPI
    {
       
        private IKalbeConnectAPIService _kalbeConnectAPIService;
        private IConfigCreatorHelper _configCreator;

        public NewKalbeConnectAPI(
            IKalbeConnectAPIService kalbeConnectAPIService,
            IConfigCreatorHelper configCreator
            )
        {
            _kalbeConnectAPIService = kalbeConnectAPIService;
            _configCreator = configCreator;
        }

        

        public ResponseData<KalbeconnectPushSmsResponse> PushSms(KalbeconnectPushSmsRequest request)
        {
            var response = new ResponseData<KalbeconnectPushSmsResponse>()
            {
                Status = new Status()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<Error>()
                }
            };

            try
            {
                var result = _kalbeConnectAPIService.PostJsonAsyncWithToken($"/sms/pushsms", request, true);

                var resultString = result.Content.ReadAsStringAsync().Result;


                if (resultString != null)
                    response = JsonConvert.DeserializeObject<ResponseData<KalbeconnectPushSmsResponse>>(resultString);

                return response;
            }
            catch (Exception ex)
            {

                response = new ResponseData<KalbeconnectPushSmsResponse>()
                {
                    Status = StatusResponse.FailedException,
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new Error() { Message = ex.Message, Type = "ERROR" });

                return response;
            }
        }

        public ResponseData<KalbeconnectPushWAResponse> PushWA(KalbeconnectPushWARequest request)
        {
            var response = new ResponseData<KalbeconnectPushWAResponse>()
            {
                Status = new Status()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<Error>()
                }
            };

            try
            {
                var result = _kalbeConnectAPIService.PostJsonAsyncWithToken($"/wa/pushwa", request, true);

                var resultString = result.Content.ReadAsStringAsync().Result;


                if (resultString != null)
                    response = JsonConvert.DeserializeObject<ResponseData<KalbeconnectPushWAResponse>>(resultString);

                return response;
            }
            catch (Exception ex)
            {

                response = new ResponseData<KalbeconnectPushWAResponse>()
                {
                    Status = StatusResponse.FailedException,
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new Error() { Message = ex.Message, Type = "ERROR" });

                return response;
            }
        }
    }
}
