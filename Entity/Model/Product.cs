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
        public Inventory Inventory { get; set; }
        public int IdCategory { get; set; }
        public Category Category { get; set; }
        public int IdImageItem { get; set; }
        public ImageItem ImageItems { get; set; }
        public int IdMovementInventory { get; set; }
        public ICollection<MovementInventory> MovementInventory { get; set; } // <- propiedad correcta
        public int IdSaleDetail { get; set; }
        public ICollection<SaleDetail> SaleDetail { get; set; } // <- propiedad correcta
    }
}
