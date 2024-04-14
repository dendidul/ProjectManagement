using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Taskgroup
    {
        public int Id { get; set; }
        public string? Taskgroupname { get; set; }
        public string? Description { get; set; }
        public int? Projectid { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public bool Isactive { get; set; }
        public bool? DelFlag { get; set; }
    }
}
