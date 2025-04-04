using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public string NameInventory { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string DescriptionInventory { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public string ZoneItem { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IdUser { get; set; }
        public required User User { get; set; }


    }
}
