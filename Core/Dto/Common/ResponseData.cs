using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Common
{
    public  class ResponseData<T>
    {
        public T Data { get; set; }
        public Status Status { get; set; }
        public DateTime TimeStamp { get; set; }

        public string TraceId { get; set; }

        public ResponseData(Status status, DateTime timeStamp)
        {
            Status = status;
            TimeStamp = timeStamp;
        }

        public ResponseData() : this(new Status(), DateTime.Now) { }

    }
}
