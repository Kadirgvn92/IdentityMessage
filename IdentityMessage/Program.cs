using FluentValidation.AspNetCore;
using FluentValidation;
using IdentityMessage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using IdentityMessage.Validations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


//burada appsettings taraf�na yazd���m�z sqlcon connectionstringi bu �ekilde tan�mlad�k
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")); 
});

//Identity i�in a�a��daki �ekilde tan�mlamalar�m�z� yapt�k
builder.Services.AddIdentity<AppUser,AppRole>(opt =>
{
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); // default olarak 5 dakika kitlenir burada biz kitli kalma s�resini de�i�tirdik
    opt.Lockout.MaxFailedAccessAttempts = 3; //ba�ar�s�z olarak girilme te�ebb�s say�s� 3 yapt�k 

}).AddEntityFrameworkStores<AppDbContext>();

//fluentvalidationda default olarak gelen hata mesajlar�n� disable yapt�k
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});

//burada bir validator verdik �rnek olarak assembly bu �rne�e g�re fluentvalidation tan�mlamas� yap�yor
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

//burada yetkilendirilmi� kullan�c� girmesi hakk�nda tan�mlama yapt�k
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

//burada yetkisiz ki�inin nereye y�nlendirece�ini belirledik.
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Login/SignIn/";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
