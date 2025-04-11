namespace Entity.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string NameItem { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int IdImageProduct { get; set; }
        public int IdInventory { get; set; }
        public bool Status { get; set; }
    }
}
