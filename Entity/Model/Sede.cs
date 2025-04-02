using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Sede
    {
        public int Id { get; set; }
        public string Name_Sede { get; set; } = string.Empty;
        public string Address_Sede { get; set; } = string.Empty;
        public string Phone_Sede { get; set; } = string.Empty;
        public string Email_Sede { get; set; } = string.Empty;
        public int Code_Sede { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Id_company { get; set; }
        public required Company Company { get; set; }

    }
}
