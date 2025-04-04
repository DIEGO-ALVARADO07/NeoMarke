using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Enum;
namespace Entity.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FistName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TypeIdentification { get; set; } = string.Empty;
        public int NumberIdentification { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}