using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.CustomerLink
{
    public class CustomerLinkResponse
    {
        public string spgPrefixSubmissionID { get; set; }
        public string spgName { get; set; }
        public string FPRSID { get; set; }
        public string PhoneNo { get; set; }
        public string ReferralCode { get; set; }
        public string BrandId { get; set; }
        public string UrlType { get; set; }
        public string ProductCategoryID { get; set; }
        public string RegionID { get; set; }
        public bool BitUseWA { get; set; }
        public DateTime dtInserted { get; set; }
        public string MessageSMSTemplate { get; set; }
        public string? RewardName { get; set; }
        public string? MembershipName { get; set; }
        public string? Link { get; set; }

        public string? Workspace { get; set; }
        public string? WATemplate { get; set; }
       // public string? WAParam { get; set; }
        public string? SMSMasking { get; set; }

    }
}
