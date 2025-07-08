using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using pacient_manager.DTOs;
using patient_manager.Models;

namespace patient_manager.Services;

public class PatientService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PatientService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PatientDto>> GetAllPatients()
    {
        var patients = await _context.Patients.ToListAsync();
        return _mapper.Map<List<Patient>, List<PatientDto>>(patients);
    }

    public async Task<PatientDto> GetPatient(int id)
    {
        var pacient =  await _context.Patients.FindAsync(id);
        if (pacient == null) throw new Exception("Paciente não encontrado!");
        return _mapper.Map<PatientDto>(pacient);
    }
    
    public async Task RegisterPatient(PatientDto patient)
    {
        var existingPacient = _context.Patients.FirstOrDefault(p => p.Document == patient.Document);
        if (existingPacient != null) throw new ValidationException($"Um paciente com o documento {existingPacient.Document}");
        
        await _context.Patients.AddAsync(_mapper.Map<Patient>(patient));
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePatient(int pacientId, PatientDto patientDto)
    {
        var existingPacient = await _context.Patients.FindAsync(pacientId);
        if (existingPacient == null) throw new Exception("Patient não encontrado!");

        if (patientDto.FirstName != null)
            existingPacient.FirstName = patientDto.FirstName;
        if (patientDto.LastName != null)
            existingPacient.LastName = patientDto.LastName;
        if (patientDto.Document != null)
            existingPacient.Document = patientDto.Document ;
        if (patientDto.DateOfBirth != null)
            existingPacient.DateOfBirth = patientDto.DateOfBirth;
        
        await _context.SaveChangesAsync();
    }
}