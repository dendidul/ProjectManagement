using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Department
    {
        public int Id { get; set; }
        public string? Departmentname { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
    }
}
