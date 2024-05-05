using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public  class HeaderAPIRequest
    {
        public string Header { get; set; }
        public string Method { get; set; }
        public string AccessUrl { get; set; }
        public string Body { get; set; }
    }
}
