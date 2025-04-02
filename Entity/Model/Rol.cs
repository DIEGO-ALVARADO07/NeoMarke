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
        public required string Name_rol { get; set; }
        public required string Description { get; set; }
        public bool Status { get; set; }
        public int Id_rolForm { get; set; }
        public required RolForm RolForm { get; set; }
        public int Id_user { get; set; }
        public required User User { get; set; }

    }
}
