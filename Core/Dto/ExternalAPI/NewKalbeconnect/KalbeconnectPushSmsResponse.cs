using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.NewKalbeconnect
{
    public class KalbeconnectPushSmsResponse
    {
        public int Type { get; set; }
        public List<ResultPushSms>? Result { get; set; }
    }

    public class ResultPushSms
    {
        public string? Id { get; set; }
        public string? Phone { get; set; }
        public string? ResponseVendor { get; set; }
        public string? Status { get; set; }
        public KalbeconnectErrorMessage? ErrorMessage { get; set; }
    }
}
