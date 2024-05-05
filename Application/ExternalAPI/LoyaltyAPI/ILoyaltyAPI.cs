using Core.Dto.Common;
using Core.Dto.CustomerLink;
using Core.Dto.ExternalAPI.LoyaltyAPI;
using Core.Dto.OrderTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ExternalAPI.LoyaltyAPI
{
    public interface ILoyaltyAPI
    {
        LoyaltyResponseData<LoyaltySubmitSubmissionResponse> SubmitSubmission(LoyaltySubmitSubmissionRequest request);
        LoyaltyResponseData<GetShortUrlAndSendNotifResponse> GetShortUrlAndSendNotif(GetShortUrlAndSendNotifRequest request);

        LoyaltyResponseData<PushTransactionOrderResponse> PushTransactionOrder(PushTransactionOrderRequest request);
    }

}
