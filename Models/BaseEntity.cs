using patient_manager.Interfaces;

namespace patient_manager.Models;

public class BaseEntity : ISoftDeletable
{
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public DateTime? DateDeleted { get; set; }
}