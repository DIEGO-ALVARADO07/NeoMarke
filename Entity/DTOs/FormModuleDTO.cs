using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class FormModuleDTO
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name_Module { get; set; } = string.Empty;
        public string Name_Form { get; set; } = string.Empty;
    }
}
