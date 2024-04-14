using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Task
    {
        public int Id { get; set; }
        public string? Taskname { get; set; }
        public int? Severityid { get; set; }
        public string? Descripition { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Duedate { get; set; }
        public int? Statusid { get; set; }
        public int? Projectid { get; set; }
        public decimal? Estimatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public int? Categoryid { get; set; }
        public int? Assignto { get; set; }
        public int? Reviewby { get; set; }
        public string? Result { get; set; }
        public int? Updateby { get; set; }
        public DateTime? Updatedate { get; set; }
        public int? Taskgroupid { get; set; }
        public int? Type { get; set; }
        public bool? DelFlag { get; set; }
    }
}
