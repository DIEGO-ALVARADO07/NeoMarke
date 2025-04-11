namespace Entity.DTOs
{
    public class RolDTO
    {
        public int Id { get; set; }
        public string NameRol { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } 
        public int IdUser { get; set; }
    }
}
