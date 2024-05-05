using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Photourl { get; set; }
        public int? Positionid { get; set; }
        public int? Departmentid { get; set; }
        public int? IsActive { get; set; }
        public string? Email { get; set; }
        public bool? DelFlag { get; set; }
    }
}
