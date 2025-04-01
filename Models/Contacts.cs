

namespace Models;

public class Contact
{
    public int ContactId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email {get; set; }

    public List<PhoneNumber> PhoneNumbers { get; } = new();
}
