using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using pacient_manager.DTOs;
using pacient_manager.Models;

namespace pacient_manager.Services;

public class PacientService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PacientService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PacientDto>> GetAllPacients()
    {
        var pacients = await _context.Pacients.ToListAsync();
        return _mapper.Map<List<Pacient>, List<PacientDto>>(pacients);
    }

    public async Task<PacientDto> GetPacient(int id)
    {
        var pacient =  await _context.Pacients.FindAsync(id);
        if (pacient == null) throw new Exception("Paciente não encontrado!");
        return _mapper.Map<PacientDto>(pacient);
    }
    
    public async Task RegisterPacient(PacientDto pacient)
    {
        var existingPacient = _context.Pacients.FirstOrDefault(p => p.Document == pacient.Document);
        if (existingPacient != null) throw new ValidationException($"Um paciente com o documento {existingPacient.Document}");
        
        await _context.Pacients.AddAsync(_mapper.Map<Pacient>(pacient));
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePacient(int pacientId, PacientDto pacientDto)
    {
        var existingPacient = await _context.Pacients.FindAsync(pacientId);
        if (existingPacient == null) throw new Exception("Pacient não encontrado!");

        if (pacientDto.FirstName != null)
            existingPacient.FirstName = pacientDto.FirstName;
        if (pacientDto.LastName != null)
            existingPacient.LastName = pacientDto.LastName;
        if (pacientDto.Document != null)
            existingPacient.Document = pacientDto.Document.Value ;
        if (pacientDto.DateOfBirth != null)
            existingPacient.DateOfBirth = pacientDto.DateOfBirth;
        
        await _context.SaveChangesAsync();
    }
}