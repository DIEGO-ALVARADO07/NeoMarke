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
        public string Name { get; set; } = string.Empty;
        public string Code_sede { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Phone_sede { get; set; }
        public string Email_sede { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string CreateAt { get; set; } = string.Empty;
        public string DeleteAt { get; set; } = string.Empty;
        public string Update_date { get; set; } = string.Empty;
    }
}
