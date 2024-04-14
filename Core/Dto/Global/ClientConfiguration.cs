using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Global
{
    public class ClientConfiguration
    {
        public int ClientConfigurationID { get; set; }
        public int ClientID { get; set; }
        public string ClientApiKey { get; set; }
        public string ClientApiSecret { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
