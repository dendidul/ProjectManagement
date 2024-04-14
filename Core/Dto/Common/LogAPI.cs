using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public class LogAPI
    {
        public string RequestHeader { get; set; }
        public string RequestParam { get; set; }
        public string ResponseParam { get; set; }
        public string ErrorMessage { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExecuteDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public string MethodBaseUrl { get; set; }


    }
}
