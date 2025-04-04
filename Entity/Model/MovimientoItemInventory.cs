using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class MovementItemInventory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int IdInventory { get; set; }
        public required Inventory Inventory { get; set; }
        public int IdItem { get; set; }
        public required Item Item { get; set; }
        public int IdMovement { get; set; }
        public required Movement Movement { get; set; } 
        public bool Status { get; set; }

    }
}
