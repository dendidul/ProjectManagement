using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Project
    {
        public int Id { get; set; }
        public int? Projectgroupid { get; set; }
        public string? Projectsname { get; set; }
        public string? Description { get; set; }
        public bool? Ispublic { get; set; }
        public bool? DelFlag { get; set; }
        public string? Createdby { get; set; }
        public DateTime? Datetimecreated { get; set; }
        public DateTime? Datetimeupdated { get; set; }
        public string? Updateby { get; set; }
    }
}
