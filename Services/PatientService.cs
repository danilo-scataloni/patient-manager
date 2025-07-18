using System.Collections;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using pacient_manager.DTOs;
using patient_manager.Interfaces;
using patient_manager.Models;

namespace patient_manager.Services;

public class PatientService : IPatientReadService, IPatientWriteService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PatientService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable> GetAllPatients()
    {
        var patients = await _context.Patients.ToListAsync();
        return _mapper.Map<List<Patient>, List<PatientDto>>(patients);
    }

    public async Task<PatientDto> GetPatient(Guid id)
    {
        var patient =  await _context.Patients.FindAsync(id);
        if (patient == null) throw new Exception("Paciente não encontrado!");
        return _mapper.Map<PatientDto>(patient);
    }
    
    public async Task RegisterPatient(PatientDto patient)
    {
        var existingPacient = _context.Patients.FirstOrDefault(p => p.Document == patient.Document);
        if (existingPacient != null) throw new ValidationException($"Um paciente com o documento {existingPacient.Document}");
        
        await _context.Patients.AddAsync(_mapper.Map<Patient>(patient));
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePatient(Guid pacientId, PatientDto patientDto)
    {
        var existingPatient = await _context.Patients.FindAsync(pacientId);
        if (existingPatient == null) throw new Exception("Patient não encontrado!");

        if (patientDto.FirstName != null)
            existingPatient.FirstName = patientDto.FirstName;
        if (patientDto.LastName != null)
            existingPatient.LastName = patientDto.LastName;
        if (patientDto.Document != null)
            existingPatient.Document = patientDto.Document ;
        if (patientDto.DateOfBirth != null)
            existingPatient.DateOfBirth = patientDto.DateOfBirth;
        
        await _context.SaveChangesAsync();
    }
}