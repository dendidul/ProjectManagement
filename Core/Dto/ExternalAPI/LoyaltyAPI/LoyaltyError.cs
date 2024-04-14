using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ExternalAPI.LoyaltyAPI
{
    public class LoyaltyError
    {
        public string? Type { get; set; }
        public string? Message { get; set; }
        public string? MessageInd { get; set; }
    }
}
