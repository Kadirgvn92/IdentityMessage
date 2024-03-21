using FluentValidation;
using IdentityMessage.ViewModels;

namespace IdentityMessage.Validations;

public class SignInValidator : AbstractValidator<SignInViewModel>
{
    public SignInValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez")
                               .Matches(@"^[^\u00c0-\u017F]+$").WithMessage("Kullanıcı Adı Türkçe karakter içermemelidir");
        RuleFor(x => x.Password)
                   .NotEmpty().WithMessage("Şifre alanı boş geçilemez")
                   .Matches("[A-Z]").WithMessage("Şifre en az 1 büyük harf içermelidir")
                   .Matches("[a-z]").WithMessage("Şifre en az 1 küçük harf içermelidir")
                   .Matches("[0-9]").WithMessage("Şifre rakam içermelidir")
                   .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 özel karakter içermelidir");
    }
}
