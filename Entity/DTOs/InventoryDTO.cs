using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public string NameInventory { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string DescriptionInventory { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public string ZoneItem { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

    }
}
