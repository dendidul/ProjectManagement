using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.Global
{
    public class ClientProfileConfiguration
    {
        public int ClientID { get; set; }
        [RegularExpression(@"^[^<>\n\r'\""\\&\u00a1-\uffff]+$", ErrorMessage = "Cannot contains character <, >, &, other than basic latin character, quote, double quote, new line, carriage return and backslash")]
        public string ClientCode { get; set; }
        [RegularExpression(@"^[^<>\n\r'\""\\&\u00a1-\uffff]+$", ErrorMessage = "Cannot contains character <, >, &, other than basic latin character, quote, double quote, new line, carriage return and backslash")]
        public string Name { get; set; }
        [RegularExpression(@"^[^<>\n\r'\""\\&\u00a1-\uffff]+$", ErrorMessage = "Cannot contains character <, >, &, other than basic latin character, quote, double quote, new line, carriage return and backslash")]
        public string Description { get; set; }
        public string IsActive { get; set; }
        public ClientConfiguration Configuration { get; set; }

    }
}
