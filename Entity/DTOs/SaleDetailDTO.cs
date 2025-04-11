namespace Entity.Model
{
    public class SaleDetailDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal {  get; set; }
        public int IdProduct { get; set; }
        public int IdSale { get; set; }
    }
}
