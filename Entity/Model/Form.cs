using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    class Form
    {
        public int id_form { get; set; }
        public string name_form { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public int id_rolForm{ get; set; }
        public RolForm rolForm { get; set; }


    }
}
