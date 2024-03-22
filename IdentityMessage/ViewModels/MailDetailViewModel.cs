using IdentityMessage.Models;

namespace IdentityMessage.ViewModels;

public class MailDetailViewModel
{
    public int MailId { get; set; }
    public string AppUserID { get; set; }
    public string ToUserEmail { get; set; }
    public string MailSubject { get; set; }
    public string MailContent { get; set; }
    public DateTime MailDate { get; set; }
    public TimeSpan MailTime { get; set; }
    public bool IsRead { get; set; }
    public bool IsImportant { get; set; }
    public bool IsDraft { get; set; }
    public bool IsJunk { get; set; }
    public bool IsTrash { get; set; }
    public AppUser AppUser { get; set; }
}
