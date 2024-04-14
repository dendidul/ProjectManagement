using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Notification
{
    public class FPRSBlastSummary
    {
        public long fPRSBlastSummaryId { get; set; }
        public string? fprsid { get; set; }
        public string? notificationType { get; set; }
        public string? phoneNumber { get; set; }
        public bool requestIsWA { get; set; }
        public bool? responseIsWA { get; set; }
        public bool is_success { get; set; }
        public int? retrySent { get; set; }
        public DateTime updateSentDate { get; set; }


    }
}
