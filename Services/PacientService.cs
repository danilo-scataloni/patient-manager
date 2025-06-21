using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using pacient_manager.Models;

namespace pacient_manager.Services;

public class PacientService
{
    private readonly ApplicationDbContext _context;

    public PacientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pacient>> GetAllPacients()
    {
        var pacients = await _context.Pacients.ToListAsync();
        return pacients;
    }
    
    public async Task RegisterPacient(Pacient pacient)
    {
        await _context.Pacients.AddAsync(pacient);
        await _context.SaveChangesAsync();

    }
}