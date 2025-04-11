﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Form
    {
        public int Id { get; set; }
        public string NameForm { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int IdRolForm { get; set; }
        public ICollection<RolForm> RolForm { get; set; } // <- propiedad correcta
    }
}
}
