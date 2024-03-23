using IdentityMessage.Models;

namespace IdentityMessage.ViewModels;

public class MailViewModel
{
    public int MailId { get; set; }
    public string AppUserID { get; set; }
    public string? ToUserEmail { get; set; }
    public string? MailSubject { get; set; }
    public string? MailContent { get; set; }
    public DateTime MailDate { get; set; }
    public TimeSpan MailTime { get; set; }
    public bool IsRead { get; set; }
    public bool IsImportant { get; set; }
    public bool IsDraft { get; set; }
    public bool IsJunk { get; set; }
    public bool IsTrash { get; set; }
    public int TotalMails { get; set; }
    public PageInfoModel PageInfo { get; set; }
    public List<Mail> Mails { get; set; }
    public Mail Mail { get; set; }
    public AppUser AppUser { get; set; }
}
