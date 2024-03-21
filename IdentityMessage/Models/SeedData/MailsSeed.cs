using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityMessage.Models.SeedData;

public class MailsSeed : IEntityTypeConfiguration<Mail>
{
    public void Configure(EntityTypeBuilder<Mail> builder)
    {
        builder.HasData(new Mail
        {
            MailId = 1,
            AppUserID = "6fdad58d-466f-4c8c-a4b5-755fe3d63912",
            ToUserEmail = "brc_kdr@gmail.com",
            MailSubject = "Stajyer başvurusu Hk.",
            MailContent = "Cv'im ektedir iyi çalışmalar",
            MailDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
            IsDraft = false,
            IsImportant = false,
            IsJunk = false,
            IsRead = false,
            IsTrash = false,
        },
        new Mail
        {
            MailId = 2,
            AppUserID = "ff56b09f-e242-4909-8ce0-47c6810c2e3a",
            ToUserEmail = "kadirgvn92@gmail.com",
            MailSubject = "Stajyer başvurusu Hk.",
            MailContent = "Cv'im ektedir iyi çalışmalar",
            MailDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
            IsDraft = false,
            IsImportant = false,
            IsJunk = false,
            IsRead = false,
            IsTrash = false,
        });
    }
}
