using Entity.Enum;

namespace Entity.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public TypeAction TypeAction { get; set; }
        public int IdReference { get; set; }
        public string Mensage { get; set; } = string.Empty;
        public bool Read { get; set; }
        public DateTime Date { get; set; }
        public int IdUser { get; set; }
    }
}
