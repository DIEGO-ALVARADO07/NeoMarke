using Entity.Model.ENUMERACIONES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    class RolForm
    {
        public int id_rolForm { get; set; }
        public Permision permision { get; set; }
        public int id_rol { get; set; }
        public Rol rol { get; set; }
        public int id_form { get; set; }
        public Form form { get; set; }

    }
}
