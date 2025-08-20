
namespace patient_manager.Models;


public interface ISoftDeletable
{
    DateTime? DateDeleted { get; set; }
}


public abstract class BaseEntity : ISoftDeletable
{
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public DateTime? DateDeleted { get; set; }
}