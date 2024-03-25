using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityMessage.Validations;

namespace IdentityMessage.Extensions;

public static class ValidationExtensions
{
    public static void ValidationExtension(this IServiceCollection services)
    {

        //fluentvalidationda default olarak gelen hata mesajlarını disable yaptık
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });

        //burada bir validator verdik örnek olarak assembly bu örneğe göre fluentvalidation tanımlaması yapıyor
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
    }
}
