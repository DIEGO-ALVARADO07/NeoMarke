using Entity.Model;

namespace Entity
{
    public class BuyOut
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdProduct { get; set; }
        public Product Product { get; set; }
    }
}
