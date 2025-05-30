﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string NameCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string NameItem { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
