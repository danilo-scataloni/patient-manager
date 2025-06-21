using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using pacient_manager.DTOs;
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

    public async Task<Pacient> GetPacient(int id)
    {
        var pacient =  await _context.Pacients.FindAsync(id);
        if (pacient == null) throw new Exception("Paciente não encontrado!");
        return pacient;
    }
    
    public async Task RegisterPacient(Pacient pacient)
    {
        await _context.Pacients.AddAsync(pacient);
        await _context.SaveChangesAsync();

    }

    public async Task UpdatePacient(int pacientId, PacientDto pacient)
    {
        var existingPacient = await _context.Pacients.FindAsync(pacientId);
        if (existingPacient == null) throw new Exception("Pacient não encontrado!");

        if (pacient.FirstName != null)
            existingPacient.FirstName = pacient.FirstName;
        if (pacient.LastName != null)
            existingPacient.LastName = pacient.LastName;
        if (pacient.Document != null)
            existingPacient.Document = pacient.Document.Value ;
        if (pacient.DateOfBirth != null)
            existingPacient.DateOfBirth = pacient.DateOfBirth;
        
        await _context.SaveChangesAsync();
    }
}