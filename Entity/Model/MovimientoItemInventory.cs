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
        public int InventoryId { get; set; }
        public required Inventory Inventory { get; set; }
        public int ItemId { get; set; }
        public required Item Item { get; set; }
        public int MovementId { get; set; }
        public required Movement Movement { get; set; } 

    }
}
