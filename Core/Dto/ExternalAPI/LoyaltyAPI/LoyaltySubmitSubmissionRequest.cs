using Core.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.LoyaltyAPI
{
    public class LoyaltySubmitSubmissionRequest
    {
        public string TransactionID { get; set; }
        public string PhoneNumber { get; set; }
        public string CardNo { get; set; }
        public string ContactCode { get; set; }
        public string GiftName { get; set; }
        public int Nominal { get; set; }
        public YearMonthDay ReceiptDate { get; set; }
        public string OuletName { get; set; }
        public string ReceiptImageUrl { get; set; }
    }
}
