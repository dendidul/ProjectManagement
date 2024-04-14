using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string? Menuname { get; set; }
        public string? Link { get; set; }
        public int? ParentId { get; set; }
        public int? Sequence { get; set; }
        public string? Controllername { get; set; }
    }
}
