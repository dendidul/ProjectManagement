using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.NewKalbeconnect
{
    public class KalbeconnectPushWAResponse
    {
        public List<KalbeconnectPushWAData>? Result { get; set; }

    }

    public class KalbeconnectPushWAData
    {
        public string? Phone { get; set; }
        public string? ID { get; set; }
        public string? Status { get; set; }
        public KalbeconnectErrorMessage? ErrorMessage { get; set; }
    }
}
