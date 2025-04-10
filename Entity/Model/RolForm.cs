using Entity.Enum;

namespace Entity.Model
{
    public class RolForm
    {
        public int Id { get; set; }
        public Permision Permision { get; set; }
        public int IdRol { get; set; }
        public Rol Rol { get; set; }
        public int IdForm { get; set; }
        public Form Form { get; set; }

    }
}
