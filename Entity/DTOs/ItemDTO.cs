using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string NameItem { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UrlImage { get; set; } = string.Empty;
        public string NameInventory { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
