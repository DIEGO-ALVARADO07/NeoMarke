using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs
{
    public class ImageItemDTo
    {
        public int Id { get; set; }
        public string UrlImage { get; set; } = string.Empty;
        public string NameItem { get; set; } = string.Empty;

    }
}
