using Microsoft.AspNetCore.Mvc;
using pacient_manager.DTOs;

namespace patient_manager.Interfaces;

public interface IPatientWriteService
{
    public Task UpdatePatient(Guid Id, PatientDto patient);
    public Task RegisterPatient(PatientDto patient);
    public Task DeletePatient(Guid Id);
}