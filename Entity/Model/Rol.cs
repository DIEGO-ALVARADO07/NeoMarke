
namespace Entity.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string NameRol { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; } 
        public int IdRolForm { get; set; }
        public ICollection<RolForm> RolForm { get; set; } // <- propiedad correcta
    }

}
}
