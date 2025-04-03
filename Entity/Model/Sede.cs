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
        public string NameSede { get; set; } = string.Empty;
        public string AddressSede { get; set; } = string.Empty;
        public string PhoneSede { get; set; } = string.Empty;
        public string EmailSede { get; set; } = string.Empty;
        public int CodeSede { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IdCompany { get; set; }
        public required Company Company { get; set; }

    }
}
