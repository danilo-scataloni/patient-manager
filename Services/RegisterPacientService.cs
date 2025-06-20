using Microsoft.AspNetCore.Http.HttpResults;
using pacient_manager.Data;
using pacient_manager.Models;

namespace pacient_manager.Services;

public class RegisterPacientService
{
    private readonly ApplicationDbContext _context;

    public RegisterPacientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task RegisterPacient(Pacient pacient)
    {
        await _context.Pacients.AddAsync(pacient);
        await _context.SaveChangesAsync();

    }
}