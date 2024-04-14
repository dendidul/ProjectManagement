using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.CustomerLink
{
    public class GetShortUrlAndSendNotifRequest
    {
        public string ReferralCode { get; set; }
        public string PhoneNo { get; set; }
        public int BrandID { get; set; }
        public string UrlType { get; set; }
        public string spgName { get; set; }
        public string spgPrefixSubmissionID { get; set; }
        public string FprsID { get; set; }
        public string ProductCategoryID { get; set; }
        public string RegionID { get; set; }
        public int BitUseWA { get; set; }
        public bool UseWA { get; set; }
        public string MessageSMSTemplate { get; set; }
    }
}
