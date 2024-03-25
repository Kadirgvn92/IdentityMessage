using FluentValidation;
using IdentityMessage.ViewModels;

namespace IdentityMessage.Validations;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordViewModel>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Şifre alanı boş geçilemez")
                    .MinimumLength(6).WithMessage("Şifre alanı en az 6 karakter olmalıdır")
                    .Matches("[A-Z]").WithMessage("Şifre en az 1 büyük harf içermelidir")
                    .Matches("[a-z]").WithMessage("Şifre en az 1 küçük harf içermelidir")
                    .Matches("[0-9]").WithMessage("Şifre rakam içermelidir")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 özel karakter içermelidir");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Şifre tekrar alanı boş geçilemez")
            .Equal(y => y.Password).WithMessage("Şifreler birbiriyle uyuşmuyor");
    }
}
