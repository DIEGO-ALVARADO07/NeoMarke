using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string NameCompany { get; set; } = string.Empty;
        public string PhoneCompany { get; set; } = string.Empty;
        public string EmailCompany { get; set; } = string.Empty;
        public int NitCompany { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
