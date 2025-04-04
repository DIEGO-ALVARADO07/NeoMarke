using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
Enum.GetNames<Permision>();

namespace Entity.DTOs
{
    public class MovementDTO
    {
        public int Id { get; set; }
        public string Permision { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
