public class clsContact
{
    private static int _idCounter = 1;

    public int ID { get; private set; }

    // Use = default! to tell the compiler: "I will initialize this later, don't worry."
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
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