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
        public required string First_name { get; set; }
        public required string Last_name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public type_identification Type { get; set; }
        public int Number_identification { get; set; }
        public bool Status { get; set; }
        public int Id_user { get; set; }
        public required User User { get; set; }
    }
}
