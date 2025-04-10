
namespace Entity.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string NameCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IdProduct { get; set; }
        public required Product Product { get; set; }

    }
}
