using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.NewKalbeconnect
{
    public class KalbeconnectPushWARequest
    {
        public string Vendor { get; set; }
        public string Workspace { get; set; }
        public List<string> PhoneList { get; set; }
        public List<string>? Params { get; set; }
        public string TemplateName { get; set; }
    }
}
