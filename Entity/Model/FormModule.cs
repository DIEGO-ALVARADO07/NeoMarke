using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class FormModule
    {
        public int Id {  get; set; }
        public bool Status { get; set; }
        public int IdForm { get; set; }
        public required Form Form { get; set; } // SIN INICIALIZAR, EVITA QUE SE CARGUE EN MEMORIA
        public int IdModule { get; set; }
        public required Module Module { get; set; } // SIN INICIALIZAR, EVITA QUE SE CARGUE EN MEMORIA

    }
}
