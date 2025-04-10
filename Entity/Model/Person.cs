using Entity.Enum;

namespace Entity.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TypeIdentification TypeIdentification { get; set; }
        public int NumberIdentification { get; set; }  
        public bool Status { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
    }
}
