namespace IdentityMessage.ViewModels;

public class UserProfileViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public int Send { get; set; }
    public int Inbox { get; set; }
    public int UnRead { get; set; }
}
