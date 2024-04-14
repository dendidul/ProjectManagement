using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Encryption
{
    public class DecryptParamUrlResponse
    {
        public string nik { get; set; }
        public string userName { get; set; }
        public string branchCode { get; set; }
        public string submissionId { get; set; }
        public string lob { get; set; }
    }
}
