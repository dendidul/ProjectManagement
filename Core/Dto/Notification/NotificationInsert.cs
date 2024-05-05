using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Notification
{
    public class NotificationInsert
    {
        public long TransactionHeaderID { get; set; }
        public string? NotificationType { get; set; }
        public string? PhoneNo { get; set; }
        public string? SenderType { get; set; }
        public string? NotificationMessage { get; set; }
        public string? EmailFrom { get; set; }
        public string? EmailTo { get; set; }
        public string? SMSMasking { get; set; }
        public string? WAWorkspace { get; set; }
        public string? WATemplate { get; set; }
        public string? WAParams { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsCompleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? FPRSId { get; set; }
        public int? BrandId { get; set; }
        public string? ProductCategoryId { get; set; }
    }
}
