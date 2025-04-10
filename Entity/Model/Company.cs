namespace Entity.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string NameCompany { get; set; } = string.Empty;
        public string PhoneCompany { get; set; } = string.Empty;
        public string EmailCompany { get; set; } = string.Empty;
        public int NitCompany { get; set; } 
        public string Logo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int IdUser { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>(); // <- propiedad correcta
    }
}
