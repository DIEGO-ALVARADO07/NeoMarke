using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class SedeDTO
    {
        public int Id { get; set; }
        public string NameSede { get; set; } = string.Empty;
        public string AddressSede { get; set; } = string.Empty;
        public string PhoneSede { get; set; } = string.Empty;
        public string EmailSede { get; set; } = string.Empty;
        public int CodeSede { get; set; }
        public bool Status { get; set; }
    }
}
