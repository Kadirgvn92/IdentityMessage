using FluentValidation;
using IdentityMessage.ViewModels;

namespace IdentityMessage.Validations;

public class NewMailValidator : AbstractValidator<MailViewModel>
{
    public NewMailValidator()
    {
        
        RuleFor(x => x.ToUserEmail).NotEmpty().WithMessage("Kime alanı boş bırakılamaz")
            .EmailAddress().WithMessage("Mail adres formatı olmalıdır");
        RuleFor(x => x.MailContent).NotEmpty().WithMessage("Mail alanı boş bırakılamaz")
            .MinimumLength(50).WithMessage("Mail içerik alanı en az 50 karakter olmalıdır.");
        RuleFor(x => x.MailSubject).NotEmpty().WithMessage("Mail konu alanı boş bırakılamaz")
            .MinimumLength(5).WithMessage("Mail konu alanı en az 5 karakter olmalıdır.")
            .MaximumLength(200).WithMessage("Mail konu alanı en fazla 200 karakter olmalıdır");
    }
}
