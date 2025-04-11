public class TimeRecord
{
    public int Id { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public int EntityId { get; set; }
    public string Action { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string OldValues { get; set; } // JSON string
    public string NewValues { get; set; } // JSON string
}
