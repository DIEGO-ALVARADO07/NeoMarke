using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class MovementItemInventoryDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int IdInventory { get; set; }
        public int IdMovement { get; set; }
        public int IdItem { get; set; }

    }
}
