using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Encryption
{
    public class DecryptionRequest
    {
        public string AccessToken { get; set; }
        public string EncryptedPassword { get; set; }
    }
}
