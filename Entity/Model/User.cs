using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entity.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime DeleteAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Status { get; set; }
        public int Id_person { get; set; }
        public required Person Person { get; set; }
        public int Id_rol { get; set; }
        public required Rol Rol { get; set; }
        public int Id_company { get; set; }
        public required Company Company { get; set; }
    }
}
