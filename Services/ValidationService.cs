using System.ComponentModel.DataAnnotations;
using patient_manager.Models;

namespace patient_manager.Services;

public interface IValidationService
{
    void ValidateCpf(string cpf);
    void ValidateDateOfBirth(DateOnly dateOfBirth);
    void ValidatePatient(PatientDto patient);
}

public class ValidationService : IValidationService
{
    public void ValidateCpf(string cpf)
    {
        if  (cpf.Length != 11 || !string.IsNullOrEmpty(cpf)) 
            throw new ValidationException("Invalid CPF");
    }

    public void ValidateDateOfBirth(DateOnly dateOfBirth)
    {
        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Today)) throw new ValidationException("Your birth date can't be literally tomorrow");
    }

    public void ValidatePatient(PatientDto patient)
    {
        ValidateCpf(patient.Document!);
        ValidateDateOfBirth(patient.DateOfBirth!.Value);
    }
}