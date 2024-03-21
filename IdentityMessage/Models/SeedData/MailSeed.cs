using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityMessage.Models.SeedData;

public class MailSeed : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var hasher = new PasswordHasher<AppUser>();
        var user1 = Guid.NewGuid().ToString();
        var user2 = Guid.NewGuid().ToString();  

        builder.HasData(
            new AppUser
            {
                Id = user1,
                UserName = "KadirGvn92",
                NormalizedUserName = "KADIRGVN92",
                PasswordHash = hasher.HashPassword(null, "Kguven1423."),
                PhoneNumber = "5382725403",
                Name = "Kadir",
                Surname = "Güven",
                Email = "kadirgvn92@gmail.com",
                NormalizedEmail = "KADIRGVN92@GMAIL.COM",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            },  
            new AppUser
            {
                Id = user2,
                UserName = "BurcuGvn92",
                NormalizedUserName = "BURCUGVN92",
                PasswordHash = hasher.HashPassword(null, "Kguven1423."),
                PhoneNumber = "5382725403",
                Name = "Burcu",
                Surname = "Güven",
                Email = "brc_kdr@gmail.com",
                NormalizedEmail = "BRC_KDR@GMAIL.COM",
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            });

    }
}
