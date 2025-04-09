using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class ImageItem
    {
        public int Id { get; set; }
        public string UrlImage { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime DeleteAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool Status { get; set; }
        public int IdItem { get; set; }
        public required Product Item { get; set; }
    }
}
