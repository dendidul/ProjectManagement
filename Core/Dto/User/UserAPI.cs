using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.User
{
    public  class UserAPI
    {      
        public int user_api_id { get; set; }
        public string api_key { get; set; }
        public string secret_key { get; set; }
        public string api_key_name { get; set; }
        public string url_method { get; set; }
    }
}
