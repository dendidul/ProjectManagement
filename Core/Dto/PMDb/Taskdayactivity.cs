using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Taskdayactivity
    {
        public int Id { get; set; }
        public int? Taskid { get; set; }
        public int? Employeeid { get; set; }
        public int? Type { get; set; }
        public DateTime? Startdate { get; set; }
        public decimal? Estimate { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
    }
}
