namespace Entity.Model
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public int IdProduct { get; set; }
        public Product Product { get; set; }
        public int IdSale { get; set; }
        public Sale Sale { get; set; }
    }
}
