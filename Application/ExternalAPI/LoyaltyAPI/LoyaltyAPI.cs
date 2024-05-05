using Core.Dto.Common;
using Core.Dto.CustomerLink;
using Core.Dto.ExternalAPI.LoyaltyAPI;
using Core.Dto.OrderTransaction;
using Infrastructure.Helper.Config;
using Infrastructure.WebServices;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ExternalAPI.LoyaltyAPI
{
    public class LoyaltyAPI : ILoyaltyAPI
    {

        private ILoyaltyAPIService _loyaltyAPIService;
        private IConfigCreatorHelper _configCreator;

        public LoyaltyAPI(
            ILoyaltyAPIService loyaltyAPIService,
            IConfigCreatorHelper configCreator
            )
        {
            _loyaltyAPIService = loyaltyAPIService;
            _configCreator = configCreator;
        }


        public LoyaltyResponseData<PushTransactionOrderResponse> PushTransactionOrder(PushTransactionOrderRequest request)
        {
            var response = new LoyaltyResponseData<PushTransactionOrderResponse>()
            {
                Status = new LoyaltyStatus()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<LoyaltyError>()
                }
            };

            try
            {
                var json = JsonConvert.SerializeObject(request);

                var result = _loyaltyAPIService.PostJsonAsyncWithToken($"/transactionorder/topuppoint", request, true);

                var resultString = result.Content.ReadAsStringAsync().Result;


                if (resultString != null)
                    response = JsonConvert.DeserializeObject<LoyaltyResponseData<PushTransactionOrderResponse>>(resultString);

                return response;
            }
            catch (Exception ex)
            {

                response = new LoyaltyResponseData<PushTransactionOrderResponse>()
                {
                    Status = new LoyaltyStatus() { Code = "400", Type = "ERROR", Message = "Error from aplication", MessageInd = "Error dari aplikasi" },
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new LoyaltyError() { Message = ex.Message, Type = "ERROR" });

                return response;
            }
        }


        public LoyaltyResponseData<LoyaltySubmitSubmissionResponse> SubmitSubmission(LoyaltySubmitSubmissionRequest request)
        {
            var response = new LoyaltyResponseData<LoyaltySubmitSubmissionResponse>()
            {
                Status = new LoyaltyStatus()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<LoyaltyError>()
                }
            };

            try
            {
                var result = _loyaltyAPIService.PostJsonAsyncWithToken($"/tada/submitsubmission", request, true);

                var resultString = result.Content.ReadAsStringAsync().Result;


                if (resultString != null)
                    response = JsonConvert.DeserializeObject<LoyaltyResponseData<LoyaltySubmitSubmissionResponse>>(resultString);

                return response;
            }
            catch (Exception ex)
            {

                response = new LoyaltyResponseData<LoyaltySubmitSubmissionResponse>()
                {
                    Status = new LoyaltyStatus() { Code = "400", Type = "ERROR", Message = "Error from aplication", MessageInd = "Error dari aplikasi" },
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new LoyaltyError() { Message = ex.Message, Type = "ERROR" });

                return response;
            }
        }

        public LoyaltyResponseData<GetShortUrlAndSendNotifResponse> GetShortUrlAndSendNotif(GetShortUrlAndSendNotifRequest request)
        {
            var response = new LoyaltyResponseData<GetShortUrlAndSendNotifResponse>()
            {
                Status = new LoyaltyStatus()
                {
                    Code = StatusCodes.Status400BadRequest.ToString(),
                    Type = "ERROR",
                    Message = "Finish with errors",
                    MessageInd = "Finish with errors",
                    Errors = new List<LoyaltyError>()
                }
            };

            try
            {
                var result = _loyaltyAPIService.PostJsonAsyncWithToken($"/shorturl/converttoshorturlandsendnotif", request, false);

                var resultString = result.Content.ReadAsStringAsync().Result;


                if (resultString != null)
                    response = JsonConvert.DeserializeObject<LoyaltyResponseData<GetShortUrlAndSendNotifResponse>>(resultString);

                return response;
            }
            catch (Exception ex)
            {

                response = new LoyaltyResponseData<GetShortUrlAndSendNotifResponse>()
                {
                    Status = new LoyaltyStatus() { Code = "400", Type = "ERROR", Message = "Error from aplication", MessageInd = "Error dari aplikasi" },
                    TimeStamp = DateTime.Now
                };

                response.Status.Errors.Add(new LoyaltyError() { Message = ex.Message, Type = "ERROR" });

                return response;
            }
        }


    }
}
