using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? Employeeid { get; set; }
        public int? Rolesid { get; set; }
        public bool DelFlag { get; set; }
    }
}
