#nullable enable
using System;

public class clsContact
{
    // The ID is now a simple property, no more auto-increment in constructor
    public int ID { get; set; }
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime AddedDate { get; set; }

    // Parameterless constructor for JSON Deserialization
    public clsContact() { }

    public clsContact(int id, string name, string phoneNumber, string email)
    {
        this.ID = id;
        this.Name = name;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
        this.AddedDate = DateTime.Now;
    }
}