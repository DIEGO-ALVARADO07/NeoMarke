using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime DeleteAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Status { get; set; }
        public int IdInventory { get; set; }
        public required Inventory Inventory { get; set; }
        public int IdCategory { get; set; }
        public required Category Category { get; set; }
        public int IdImageItem { get; set; }
        public required ImageItem ImageItems { get; set; }
    }
}
