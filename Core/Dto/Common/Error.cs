using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public class Error
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string MessageInd { get; set; }

        public Error()
        {
            Type = string.Empty;
            Message = string.Empty;
            MessageInd = string.Empty;
        }
    }
}
