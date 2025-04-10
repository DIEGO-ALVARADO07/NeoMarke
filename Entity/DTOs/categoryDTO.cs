namespace Entity.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string NameCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int IdProduct { get; set; } 
        public bool Status { get; set; }
    }
}
