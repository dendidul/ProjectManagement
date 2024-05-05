using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Notification
{
    public class InsertFPRSBlastLog
    {
        public string? ReferralCode { get; set; }
        public string? PhoneNo { get; set; }
        public int BrandID { get; set; }
        public string? URLType { get; set; }
        public string? FPRSID { get; set; }
        public string? ProductCategoryID { get; set; }
        public string? RegionID { get; set; }
        public bool RequestUseWA { get; set; }
        public bool ResponseUseWA { get; set; }
        public string? WAWorkspace { get; set; }
        public string? WATemplate { get; set; }
        public string? WAParam { get; set; }
        public string? SMSMasking { get; set; }
        public string? MessageSMSTemplate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ShortURL { get; set; }
        public string StatusKey { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RequestParam { get; set; }
       
    }

   

}
