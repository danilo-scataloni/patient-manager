using Microsoft.EntityFrameworkCore;
using pacient_manager.Models;

namespace pacient_manager.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }
    public DbSet<Pacient> Pacients { get; set; }
}