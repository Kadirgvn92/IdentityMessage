using FluentValidation;
using IdentityMessage.ViewModels;

namespace IdentityMessage.Validations;

public class ForgetPasswordValidation : AbstractValidator<ForgetPasswordViewModel>
{
    public ForgetPasswordValidation()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş olamaz.").EmailAddress().WithMessage("Email formatı ile giriş yapınız.");
    }
}
