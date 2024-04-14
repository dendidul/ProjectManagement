using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Position
    {
        public int Id { get; set; }
        public string? Positionname { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
    }
}
