using Entity.Enum;


namespace Entity.DTOs
{
    public class MovementInventoryDTO
    {
        public int Id { get; set; }
        public TypeMovement TypeMovement { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int IdInventory { get; set; }
        public int IdProduct { get; set; }
        public int IdUser { get; set; }

    }
}
