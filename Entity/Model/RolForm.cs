using Entity.Model.ENUMERACIONES;
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
        public Permision Permision { get; set; }
        public int Id_rol { get; set; }
        public required Rol Rol { get; set; }
        public int Id_form { get; set; }
        public required Form Form { get; set; }

    }
}
