using System;

namespace Assignment4.Models;

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

}