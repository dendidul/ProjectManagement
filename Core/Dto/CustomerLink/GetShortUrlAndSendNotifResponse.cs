using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.CustomerLink
{
    public class GetShortUrlAndSendNotifResponse
    {
        public string txtShortUrl { get; set; }
        public string phoneNo { get; set; }
        public bool useWA { get; set; }
        public string waWorkspace { get; set; }
        public string waTemplate { get; set; }

        public string[] waParams { get; set; }
        public string smsMasking { get; set; }
        public string smsMessage { get; set; }

    }
}
