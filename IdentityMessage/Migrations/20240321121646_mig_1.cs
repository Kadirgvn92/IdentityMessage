using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityMessage.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fdad58d-466f-4c8c-a4b5-755fe3d63912");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff56b09f-e242-4909-8ce0-47c6810c2e3a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0cd749ea-67cd-48e0-b354-0200ecc5b98d", 0, "9f6db50f-df05-4481-b4e8-48e289d578e1", "kadirgvn92@gmail.com", false, false, null, "Kadir", "KADIRGVN92@GMAIL.COM", "KADIRGVN92", "AQAAAAEAACcQAAAAECNSTCH0yygVZvwLrVf3Pd6Q3+zNmirTvzZ9siyVQLCkN/IV+Mk+xT56iK0nhPqjnw==", "5382725403", false, "d7745bfa-ced6-4014-a691-c064c3026bd4", "Güven", false, "KadirGvn92" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "25e55af0-bd93-479f-9120-e85a121c0952", 0, "50fe22ae-1a22-4ff1-b61c-40f1d3ad0f9a", "brc_kdr@gmail.com", false, false, null, "Burcu", "BRC_KDR@GMAIL.COM", "BURCUGVN92", "AQAAAAEAACcQAAAAEMvaMhIAODE/IBAUlhRNCGK7HERWER7D+L+KskeBc7Ud905e+fSRMX2boa4sI05BBA==", "5382725403", false, "27d5aee5-ef87-4800-9f53-a86416d75cdd", "Güven", false, "BurcuGvn92" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0cd749ea-67cd-48e0-b354-0200ecc5b98d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25e55af0-bd93-479f-9120-e85a121c0952");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6fdad58d-466f-4c8c-a4b5-755fe3d63912", 0, "112bbb14-1db8-4e89-919f-63860e526952", "kadirgvn92@gmail.com", false, false, null, "Kadir", "KADIRGVN92@GMAIL.COM", "KADIRGVN92", "AQAAAAEAACcQAAAAEF+HmcWldkwO6zfQxM+UpKq9KD2g/Sw5Gu+xjVMZ79nppZxmEDRISs4SeLiLo2Br3Q==", "5382725403", false, "f12bf291-d3f2-4df7-b914-39b97b412f78", "Güven", false, "KadirGvn92" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ff56b09f-e242-4909-8ce0-47c6810c2e3a", 0, "1078f4bd-20c4-45f1-8fe4-f76530e3cc8e", "brc_kdr@gmail.com", false, false, null, "Burcu", "BRC_KDR@GMAIL.COM", "BURCUGVN92", "AQAAAAEAACcQAAAAEMNEH/UsIQDcVOlJNgVWikjvQYqqinDEzB3DrefhMX5+6Rc8zkb6+55Ng680gSxRFg==", "5382725403", false, "dcf96ba6-fa90-4f51-86ca-1e13b62be023", "Güven", false, "BurcuGvn92" });
        }
    }
}
