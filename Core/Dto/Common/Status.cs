using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public class Status
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string MessageInd { get; set; }
        public string Type { get; set; }
        public List<Error> Errors { get; set; }

        public Status()
        {
            Code = string.Empty;
            Message = string.Empty;
            MessageInd = string.Empty;
            Type = string.Empty;
            Errors = new List<Error>();
        }
    }
}
