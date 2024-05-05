using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? Newsfeedid { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
        public int? Employeeid { get; set; }
    }
}
