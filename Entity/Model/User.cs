﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entity.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime DeleteAt { get; set; }
        public bool Status { get; set; }
        public int IdPerson { get; set; }
        public required Person Person { get; set; }
        public int IdRol { get; set; }
        public required Rol Rol { get; set; }
        public int IdCompany { get; set; }
        public required Company Company { get; set; }
    }
}
