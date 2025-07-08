namespace pacient_manager.DTOs;

public class PatientDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Document { get; set; }
    public DateOnly DateOfBirth { get; set; }
}