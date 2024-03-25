using FluentValidation;
using IdentityMessage.ViewModels;

namespace IdentityMessage.Validations;

public class LoginValidator : AbstractValidator<SignUpViewModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.AcceptTerms)
            .Must(x => x)
            .WithMessage("Şartları kabul etmelisiniz.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Mail alanı boş geçilemez");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı alanı boş geçilemez")
                                .Matches(@"^[^\u00c0-\u017F]+$").WithMessage("Kullanıcı Adı Türkçe karakter içermemelidir")
                                .MaximumLength(20).WithMessage("Lütfen en fazla 20 karatker girişi yapın")
                                .MinimumLength(5).WithMessage("Lütfen en az 5 karatker girişi yapın");
        RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Şifre alanı boş geçilemez")
                    .MinimumLength(6).WithMessage("Şifre alanı en az 6 karakter olmalıdır")
                    .Matches("[A-Z]").WithMessage("Şifre en az 1 büyük harf içermelidir")
                    .Matches("[a-z]").WithMessage("Şifre en az 1 küçük harf içermelidir")
                    .Matches("[0-9]").WithMessage("Şifre rakam içermelidir")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 özel karakter içermelidir");
    }
}
