using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityMessage.Models.SeedData;

public class SeedMailData : IEntityTypeConfiguration<Mail>
{
    public void Configure(EntityTypeBuilder<Mail> builder)
    {
        builder.HasData(
                new Mail()
                {
                    MailId = 10,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Mail Konusu 1",
                    MailContent = "Gelen Mail içeriği 1",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 11,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 2",
                    MailContent = "Gelen Mail içeriği 2",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 12,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 3",
                    MailContent = "Gelen Mail içeriği 4",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = false,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 13,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 4",
                    MailContent = "Gelen Mail içeriği 4",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 14,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 5",
                    MailContent = "Gelen Mail içeriği 5",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 15,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 6",
                    MailContent = "Gelen Mail içeriği 6",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 16,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 7",
                    MailContent = "Gelen Mail içeriği 7",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 17,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 8",
                    MailContent = "Gelen Mail içeriği 8",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                },
                new Mail()
                {
                    MailId = 18,
                    AppUserID = "5bb5ec19-477c-4838-82ee-86840f5ebb08",
                    MailSubject = "Gelen Konu başlığı 9",
                    MailContent = "Gelen Mail içeriği 9",
                    MailDate = new DateTime(2024, 01, 01, 13, 30, 00),
                    IsRead = true,
                    ToUserEmail = "brc_kdr@hotmail.com",
                    IsDraft = false,
                    IsImportant = false,
                    IsTrash = false,
                });
    }
}