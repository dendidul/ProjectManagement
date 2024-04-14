using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.OrderTransaction
{
    public  class PushTransactionOrderResponse
    {
        public string transactionNo { get; set; }
        public string fileNo { get; set; }
        public string status { get; set; }
        public PointResponse point { get; set; }
    }

    public class PointResponse
    {
       
        public int pointTransaction { get; set; }
        public int pointBefore { get; set; }
        public int totalPoint { get; set; }
        public string periodePoint { get; set; }
        public string periodeStartDate { get; set; }
        public string periodeEndDate { get; set; }

    }
}
