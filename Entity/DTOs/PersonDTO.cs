using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    class PersonDTO
    {
        public int Id { get; set; }
        public string Fist_Name { get; set; } = string.Empty;
        public string Last_name { get; set; } = string.Empty;
        public int Phone_number { get; set; }
        public string Email { get; set; } = string.Empty;
        public type_identification Type { get; set; }
        public int Number_Indification { get; set; }
        public bool Status { get; set; }
    }
}
