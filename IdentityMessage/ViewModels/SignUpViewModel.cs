using System.ComponentModel.DataAnnotations;

namespace IdentityMessage.ViewModels;

public class SignUpViewModel
{
    [Required(ErrorMessage = "Ad alanı boş bırakılamaz")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyad alanı boş bırakılamaz")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Kullanıcı Adı alanı boş bırakılamaz")]
    public string UserName { get; set; }

    [EmailAddress(ErrorMessage = "Email formatı yanlıştır")]
    [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Telefon numarası alanı boş bırakılamaz")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Şifre alanı boş bırakılamaz")]
    public string Password { get; set; }


    [Compare(nameof(Password),ErrorMessage = "Şifreler uyuşmamaktadır.")]
    [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
    public string ConfirmPassword { get; set; }
}
