using Core.Dto.Common;
using Core.Dto.ExternalAPI.NewKalbeconnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ExternalAPI.NewKalbeConnect
{
    public interface INewKalbeConnectAPI
    {
        ResponseData<KalbeconnectPushSmsResponse> PushSms(KalbeconnectPushSmsRequest request);
        ResponseData<KalbeconnectPushWAResponse> PushWA(KalbeconnectPushWARequest request);
    }
}
