namespace Entity.DTOs
{
    public class BuyOutDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; } 
        public int IdUser { get; set; }
        public int IdProduct { get; set; }

    }
}
