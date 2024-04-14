using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Rolesprojectemployee
    {
        public int Id { get; set; }
        public int? Roleid { get; set; }
        public int? Empid { get; set; }
        public int? Projectid { get; set; }
        public bool? DelFlag { get; set; }
        public bool? IsActive { get; set; }
    }
}
