using Microsoft.AspNetCore.Identity;

namespace IdentityMessage.Models;

public class AppUser : IdentityUser
{

    public string Name { get; set; }
    public string Surname { get; set; }
    public string? City { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime? BirthDate { get; set; }
    public byte? Gender { get; set; }
    public List<Mail> Mail { get; set; }
}
