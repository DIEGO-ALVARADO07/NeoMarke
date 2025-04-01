using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    class User
    {
        public int id_user { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime createAt { get; set; }
        public DateTime deleteAt { get; set; }
        public DateTime updateAt { get; set; }
        public bool status { get; set; }
        public int id_person { get; set; }
        public Person person { get; set; }
        public int id_rol { get; set; }
        public Rol rol { get; set; }


    }
}
