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
        public string NameModule { get; set; } = string.Empty;
        public string NameForm { get; set; } = string.Empty;
}
}
