using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.LoyaltyAPI
{
    public class LoyaltySubmitSubmissionResponse
    {
        public string? TransactionID { get; set; }
        public int SurveyAnswerId { get; set; }
        public string? Brand { get; set; }
        public int PerkId { get; set; }
        public bool Verification { get; set; }
        public string? ProgramId { get; set; }
        public string? ProgramName { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
    }
}
