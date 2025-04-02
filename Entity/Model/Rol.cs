using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Name_rol { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int Id_user { get; set; }
        public required User User { get; set; }

    }
}
