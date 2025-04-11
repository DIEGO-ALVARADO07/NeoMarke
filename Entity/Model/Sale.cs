

namespace Entity.Model
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Totaly { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdSaleDetail { get; set; }
        public ICollection<SaleDetail> SaleDetail { get; set; } // <- propiedad correcta
    }
}
