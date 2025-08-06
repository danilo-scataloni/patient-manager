using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using patient_manager.Models;

namespace pacient_manager.Data;

public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    public DbSet<Patient> Patients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder
                .Entity<Patient>()
                .HasQueryFilter(p => p.DateDeleted == null);
        }
    }
}