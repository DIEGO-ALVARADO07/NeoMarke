using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    class Rol
    {
        public int id_rol { get; set; }
        public string name_rol { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public int id_rolForm { get; set; }
        public RolForm rolForm { get; set; }
        public int id_user { get; set; }
        public User user { get; set; }

    }
}
