using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Type
    {
        public int Id { get; set; }
        public string? Typename { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
    }
}
