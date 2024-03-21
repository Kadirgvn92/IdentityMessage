using System.ComponentModel.DataAnnotations;

namespace IdentityMessage.ViewModels;

public class SignUpViewModel
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    
    public bool AcceptTerms { get; set; }
    public string ConfirmPassword { get; set; }
}
