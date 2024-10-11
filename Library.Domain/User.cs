using Microsoft.AspNetCore.Identity;

namespace Library.Domain;

public class User: IdentityUser
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
}