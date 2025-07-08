using Microsoft.EntityFrameworkCore;
using patient_manager.Models;

namespace pacient_manager.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    public DbSet<Patient> Patients { get; set; }
}