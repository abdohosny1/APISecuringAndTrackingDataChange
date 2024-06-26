

namespace MyAPISecuringAndTrackingDataChange.Core.Model
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string User => "System-User";
        public required string EntityType { get; set; }
        public required string Action { get; set; }
        public required DateTime TimeStamp { get; set; }
        public required string Changes { get; set; }
    }

}
