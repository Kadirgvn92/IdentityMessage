namespace IdentityMessage.Services;

public interface IEmailService
{
    Task SendResetEmail(string resetEmailLink, string To);
}
