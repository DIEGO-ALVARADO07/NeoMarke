using Entity.Enum;


namespace Entity.Model
{
    public class MovementInventory
    {
        public int Id { get; set; }
        public TypeMovement TypeMovement { get; set; } 
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int IdInventory { get; set; }
        public required Inventory Inventory { get; set; }
        public int IdProduct { get; set; }
        public required Product Product { get; set; }
        public int IdUser { get; set; }
        public required User User { get; set; } 
    }
}
