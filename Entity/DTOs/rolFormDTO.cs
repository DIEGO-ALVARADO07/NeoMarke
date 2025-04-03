using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Model.ENUMERACIONES;
namespace Entity.DTOs
{
    public class RolFormDTO
    {
        public int Id { get; set; }
        public required Permision Permision { get; set; }
        public string NameRol { get; set; } = string.Empty;
        public string NameForm { get; set; } = string.Empty;
    }
}
