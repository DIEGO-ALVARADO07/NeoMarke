using Entity.Enum;

namespace Entity.DTOs
{
    public class RolFormDTO
    {
        public int Id { get; set; }
        public Permision Permision { get; set; }
        public int IdRol { get; set; }
        public int IdForm { get; set; }
    }
}
