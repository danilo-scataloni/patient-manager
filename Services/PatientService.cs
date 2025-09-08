using System.Collections;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pacient_manager.Data;
using patient_manager.Models;

namespace patient_manager.Services;

public interface IPatientService
{
    Task<PatientDto> GetPatient(Guid id);
    Task<IEnumerable> GetAllPatients();
    public Task UpdatePatient(Guid id, PatientDto patient);
    public Task CreatePatient(PatientDto patient);
    public Task DeletePatient(Guid id);
}

public class PatientService(ApplicationDbContext context, IMapper mapper, IValidationService validationService)
    : IPatientService
{

    public async Task<IEnumerable> GetAllPatients()
    {
        var patients = await context.Patients.ToListAsync();
        return mapper.Map<List<Patient>, List<PatientDto>>(patients);
    }

    public async Task<PatientDto> GetPatient(Guid id)
    {
        var patient =  await context.Patients.FindAsync(id);
        if (patient == null) throw new Exception("Paciente não encontrado!");
        return mapper.Map<PatientDto>(patient);
    }
    
    public async Task CreatePatient(PatientDto patient)
    {
        var existingPatient = context.Patients.FirstOrDefault(p => p.Document == patient.Document);
        if (existingPatient != null) throw new ArgumentException($"Um paciente com o documento {existingPatient.Document}");
        validationService.ValidatePatient(patient);
        
        await context.Patients.AddAsync(mapper.Map<Patient>(patient));
        await context.SaveChangesAsync();
    }

    public async Task DeletePatient(Guid id)
    {
        var patient = await context.Patients.FindAsync(id);
        if (patient == null) throw new KeyNotFoundException("Paciente não encontrado!");
        patient.DateDeleted = DateTime.Now;
        await context.SaveChangesAsync();
    }

    public async Task UpdatePatient(Guid pacientId, PatientDto patientDto)
    {
        var existingPatient = await context.Patients.FindAsync(pacientId);
        if (existingPatient == null) throw new KeyNotFoundException("Patient não encontrado!");
        if (patientDto.FirstName != null)
            existingPatient.FirstName = patientDto.FirstName;
        if (patientDto.LastName != null)
            existingPatient.LastName = patientDto.LastName;
        if (patientDto.Document != null)
            existingPatient.Document = patientDto.Document ;
        if (patientDto.DateOfBirth != null)
            existingPatient.DateOfBirth = patientDto.DateOfBirth.Value;
        
        existingPatient.DateCreated =  DateTime.Now;
        await context.SaveChangesAsync();
    }
}