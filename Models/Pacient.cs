using System.Text.Json.Serialization;

namespace pacient_manager.Models;

public class Pacient
{
    [JsonIgnore]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string  LastName { get; set; }
    public int Document { get; set; }
    public string DateOfBirth { get; set; }
    
    public Pacient() {}
    public Pacient(int id, string firstName, string lastName, int document, string dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        DateOfBirth = dateOfBirth;
    }
}