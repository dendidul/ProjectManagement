using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Role
    {
        public int Id { get; set; }
        public string? Rolesname { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
    }
}
