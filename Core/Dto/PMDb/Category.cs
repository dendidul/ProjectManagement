﻿using System;
using System.Collections.Generic;

namespace Core.Dto.PMDb
{
    public partial class Category
    {
        public int Id { get; set; }
        public string? Categoryname { get; set; }
        public string? Description { get; set; }
        public bool? DelFlag { get; set; }
    }
}
