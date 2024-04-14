using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Document
    {
        public int Id { get; set; }
        public string? Documentname { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
        public bool? IsPublic { get; set; }
        public int? Projectid { get; set; }
    }
}
