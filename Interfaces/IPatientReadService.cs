using System.Collections;
using pacient_manager.DTOs;

namespace patient_manager.Interfaces;

public interface IPatientReadService
{
    Task<PatientDto> GetPatient(Guid id);
    Task<IEnumerable> GetAllPatients();
}