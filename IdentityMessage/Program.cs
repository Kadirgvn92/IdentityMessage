using FluentValidation.AspNetCore;
using FluentValidation;
using IdentityMessage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using IdentityMessage.Validations;
using Microsoft.AspNetCore.Identity;
using IdentityMessage.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


//burada appsettings taraf�na yazd���m�z sqlcon connectionstringi bu �ekilde tan�mlad�k
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")); 
});

builder.Services.ValidationExtension(); //FluentValidation ile ilgili extensions

builder.Services.AddIdentityExtensitions(); //Idenitty ile ilgili extensions


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
