using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityMessage.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "MailId", "AppUserID", "IsDraft", "IsImportant", "IsJunk", "IsRead", "IsTrash", "MailContent", "MailDate", "MailSubject", "MailTime", "ToUserEmail" },
                values: new object[,]
                {
                    { 10, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 1", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Mail Konusu 1", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 11, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 2", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 2", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 12, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, false, false, "Gelen Mail içeriği 4", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 3", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 13, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 4", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 4", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 14, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 5", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 5", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 15, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 6", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 6", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 16, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 7", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 7", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 17, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 8", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 8", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" },
                    { 18, "5bb5ec19-477c-4838-82ee-86840f5ebb08", false, false, false, true, false, "Gelen Mail içeriği 9", new DateTime(2024, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified), "Gelen Konu başlığı 9", new TimeSpan(0, 0, 0, 0, 0), "brc_kdr@hotmail.com" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Mails",
                keyColumn: "MailId",
                keyValue: 18);
        }
    }
}
