using System.Text.Json.Serialization;

namespace pacient_manager.Models;

public class Pacient
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string  LastName { get; set; }
    public string Document { get; set; }
    public DateOnly DateOfBirth { get; set; }
    
    public Pacient() {}
    public Pacient(Guid id, string firstName, string lastName, string document, DateOnly dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        DateOfBirth = dateOfBirth;
    }
}