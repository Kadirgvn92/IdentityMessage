using FluentValidation.AspNetCore;
using FluentValidation;
using IdentityMessage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using IdentityMessage.Validations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


//burada appsettings tarafýna yazdýðýmýz sqlcon connectionstringi bu þekilde tanýmladýk
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")); 
});

//Identity için aþaðýdaki þekilde tanýmlamalarýmýzý yaptýk
builder.Services.AddIdentity<AppUser,AppRole>(opt =>
{
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); // default olarak 5 dakika kitlenir burada biz kitli kalma süresini deðiþtirdik
    opt.Lockout.MaxFailedAccessAttempts = 3; //baþarýsýz olarak girilme teþebbüs sayýsý 3 yaptýk 

}).AddEntityFrameworkStores<AppDbContext>();

//fluentvalidationda default olarak gelen hata mesajlarýný disable yaptýk
builder.Services.AddFluentValidationAutoValidation(config =>
{
    config.DisableDataAnnotationsValidation = true;
});

//burada bir validator verdik örnek olarak assembly bu örneðe göre fluentvalidation tanýmlamasý yapýyor
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

//burada yetkilendirilmiþ kullanýcý girmesi hakkýnda tanýmlama yaptýk
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

//burada yetkisiz kiþinin nereye yönlendireceðini belirledik.
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
