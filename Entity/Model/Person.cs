using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    class Person
    {
        public int id_person { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public type_identification type { get; set; }
        public int number_identification { get; set; }
        public bool Status { get; set; }
        public int id_user { get; set; 
        public User user { get; set; }
    }
}
