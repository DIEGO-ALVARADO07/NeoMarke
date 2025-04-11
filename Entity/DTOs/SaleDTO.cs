namespace Entity.Model
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Totaly { get; set; }
        public int IdUser { get; set; }
        public int IdSaleDetail { get; set; }
    }
}
