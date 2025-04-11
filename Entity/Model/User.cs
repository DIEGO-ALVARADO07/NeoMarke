namespace Entity.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime DeleteAt { get; set; }
        public bool Status { get; set; }
        public int IdPerson { get; set; }
        public Person Person { get; set; }
        public int IdRol { get; set; }
        public Rol Rol { get; set; } 
        public int IdCompany { get; set; }
        public Company Company { get; set; }
        public int IdNotification { get; set; }
        public ICollection<Notification> Notification { get; set; } // <- propiedad correcta
        public int IdSale { get; set; }
        public ICollection<Sale> Sale { get; set; } // <- propiedad correcta
        public int IdMovementInventory { get; set; }
        public ICollection<MovementInventory> MovementInventory { get; set; } // <- propiedad correcta
    }
}
