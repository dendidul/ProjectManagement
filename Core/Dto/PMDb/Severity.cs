using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Severity
    {
        public int Id { get; set; }
        public string? Severityname { get; set; }
        public string? Descripiton { get; set; }
        public bool? DelFlag { get; set; }
    }
}
