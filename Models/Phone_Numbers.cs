

namespace Models;

public class PhoneNumber
{
    public int PhoneNumberId { get; set; }
    public long TenDigitNumber { get; set; }
    public string Location { get; set; }

    public int ContactId { get; set; }
    public Contact Contacts { get; set; }
}