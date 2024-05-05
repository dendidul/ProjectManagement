using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Encryption
{
    public class SignatureRequest
    {
        public string AccessToken { get; set; }
        public string HttpMethod { get; set; }
        public string RelativeUrl { get; set; }
        public string RequestBody { get; set; }
        public string DateTimeTick { get; set; }
    }
}
