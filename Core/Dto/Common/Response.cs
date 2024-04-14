using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public class Response
    {
        public Status Status { get; set; }

        public DateTime TimeStamp { get; set; }

        #region constructor
        public Response(Status status, DateTime timeStamp)
        {
            Status = status;
            TimeStamp = timeStamp;
        }

        public Response() : this(new Status(), DateTime.Now) { }
        #endregion
    }
}
