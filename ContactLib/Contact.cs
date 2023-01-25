namespace ContactLib.Models;

/// <summary>Represents a contact.</summary>
public class Contact
{

    public Guid ID { get; set; } = Guid.NewGuid();
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public string FullName => FirstName + " " + LastName;

    public override string ToString() => FullName;

    public (Guid ID, string? FirstName, string? LastName, string? Email, string? PhoneNumber, string? Address) ToTuple() =>
        (ID, FirstName, LastName, Email, PhoneNumber, Address);

    public (string? FirstName, string? LastName, string? Email, string? PhoneNumber, string? Address) ToTupleWithoutID() =>
        (FirstName, LastName, Email, PhoneNumber, Address);

}