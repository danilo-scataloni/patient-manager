namespace patient_manager.Models;

public class Patient : BaseEntity
{
    public Guid Id { get; init; }
    public string FirstName { get; set; }
    public string  LastName { get; set; }
    public string Document { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public Patient(){}
    public Patient(Guid id, string firstName, string lastName, string document, DateOnly dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        DateOfBirth = dateOfBirth;
    }
}

public class PatientDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Document { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}