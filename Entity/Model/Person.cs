using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string First_name { get; set; } = string.Empty;
        public string Last_name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public type_identification Type { get; set; }
        public int Number_identification { get; set; }
        public bool Status { get; set; }
        public int Id_user { get; set; }
        public required User User { get; set; }
    }
}
