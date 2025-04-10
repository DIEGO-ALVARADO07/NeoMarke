using Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Enum;

namespace Entity.DTOs
{
    public class RolFormDTO
    {
        public int Id { get; set; }
        public Permision Permision { get; set; }
        public int IdRol { get; set; }
        public int IdForm { get; set; }
    }
}
