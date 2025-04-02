using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Movement
    {
        public int Id { get; set; }
        public string Type_Movement { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Status { get; set; }
    }
}
