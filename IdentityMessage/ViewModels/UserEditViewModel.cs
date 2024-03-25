namespace IdentityMessage.ViewModels;

public class UserEditViewModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? City { get; set; }
    public IFormFile? Image { get; set; }
    public string? ImageUrl { get; set; }
    public byte? Gender{ get; set; }
}
