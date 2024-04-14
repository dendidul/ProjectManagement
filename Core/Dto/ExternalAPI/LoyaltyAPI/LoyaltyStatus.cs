using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.LoyaltyAPI
{
    public class LoyaltyStatus
    {

        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? MessageInd { get; set; }
        public string? Type { get; set; }
        public List<LoyaltyError> Errors { get; set; }

        public LoyaltyStatus()
        {
            Code = string.Empty;
            Message = string.Empty;
            MessageInd = string.Empty;
            Type = string.Empty;
            Errors = new List<LoyaltyError>();
        }

    }
}
