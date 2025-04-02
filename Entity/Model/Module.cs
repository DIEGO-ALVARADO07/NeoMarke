using Entity.Model.ENUMERACIONES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    class Module
    {
       public int Id_module { get; set; }
        public string Name_Module { get; set; } = string.Empty;
        public bool Status { get; set; }

    }
}
