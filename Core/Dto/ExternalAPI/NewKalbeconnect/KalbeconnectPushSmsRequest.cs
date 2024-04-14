using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.NewKalbeconnect
{
    public class KalbeconnectPushSmsRequest
    {
        public string VendorName { get; set; }
        public string Masking { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public int Blast { get; set; }
        public List<string> PhoneNoList { get; set; }
    }
}
