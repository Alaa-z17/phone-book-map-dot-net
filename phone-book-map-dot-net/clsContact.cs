#nullable enable

public class clsContact
{
    private static int _idCounter = 1;

    public int ID { get; private set; }

    // These properties are required; they will be set in the constructor.
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime AddedDate { get; set; }

    public clsContact(string name, string phoneNumber, string email)
    {
        ID = _idCounter++;
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        AddedDate = DateTime.Now;
    }
}