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


builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Login/SignIn/"; //burada yetkisiz ki�inin nereye y�nlendirece�ini belirledik.
    opt.LogoutPath = "/Login/LogOut"; //burada biz hangi metotla ��k�� yap�laca��n� belirledik.

    var cookieBuilder = new CookieBuilder(); //Cookie ayarlar� i�in bir builder t�rettik.
    cookieBuilder.Name = "IdentityMailCookie"; //Cookie ismini belirledik.

    opt.Cookie = cookieBuilder; //Cookiemizin tan�mlad���m�z cookieBuildera tan�mlad�k.
    opt.ExpireTimeSpan = TimeSpan.FromDays(60); //cookiemizin ya�am s�resini belirledik 60 g�n olarak.
    opt.SlidingExpiration = true; //cookimizn her giri�inde ya�am s�resi uzat�ls�n m� diye soruyoruz. biz evet dedik
    //yukar�daki expritetime mant��� : Kullan�c� giri� yapt� ve 60 g�n boyunca cookie sakland� Okey. ancak 60 g�n sonunda 
    //bize tekrar �ifre soracak sliding olay� ise bu 60 g�nl�k zaman zarf�nda girerse sayac� 0 dan ba�lat�yor 
    //�rne�in kullan�c� 30. g�nde giri� yapt� art�k 60 tekrar ba�tan ba�lad� yani kullan�c� 30 + 60 = 90 g�n �ifresiz girebilir.


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
