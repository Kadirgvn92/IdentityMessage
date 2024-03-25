using FluentValidation;
using IdentityMessage.ViewModels;

namespace IdentityMessage.Validations;

public class UserEditValidator : AbstractValidator<UserEditViewModel>
{
    public UserEditValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş geçilemez")
            .EmailAddress().WithMessage("Email formatı ile giriş yapmalısınız");

        RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez")
                                .Matches(@"^[^\u00c0-\u017F]+$").WithMessage("Kullanıcı Adı Türkçe karakter içermemelidir")
                                .MaximumLength(20).WithMessage("Lütfen en fazla 20 karatker girişi yapın")
                                .MinimumLength(5).WithMessage("Lütfen en az 5 karatker girişi yapın");


    }
}
