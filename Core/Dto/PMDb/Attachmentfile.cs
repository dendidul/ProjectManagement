using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Attachmentfile
    {
        public int Id { get; set; }
        public bool? Istaskdocument { get; set; }
        public int? Taskid { get; set; }
        public bool? Isbugdocument { get; set; }
        public string? Bugid { get; set; }
        public bool? Isdocumentid { get; set; }
        public int? Documentid { get; set; }
        public string? Url { get; set; }
        public string? Filetype { get; set; }
        public int? Attachmenttype { get; set; }
    }
}
