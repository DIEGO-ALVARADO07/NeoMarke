using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name_Company { get; set; } = string.Empty;
        public string Phone_Company { get; set; } = string.Empty;
        public string Email_Company { get; set; } = string.Empty;
        public int Nit_Company { get; set; } 
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
