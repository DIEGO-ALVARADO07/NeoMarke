using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class RolForm
    {
        public int Id { get; set; }
        public required Permision Permision { get; set; }
        public int IdRol { get; set; }
        public required Rol Rol { get; set; }
        public int IdForm { get; set; }
        public required Form Form { get; set; }

    }
}
