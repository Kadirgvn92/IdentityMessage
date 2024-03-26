
using IdentityMessage.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Configuration;

namespace IdentityMessage.Extensions;

public static class IdentityExtensions
{
    public static void AddIdentityExtensitions(this IServiceCollection services, IConfiguration configuration)
    {

        //burada yetkilendirilmiş kullanıcı girmesi hakkında tanımlama yaptık
        services.AddMvc(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
            config.Filters.Add(new AuthorizeFilter(policy));
        });


        //Burada Token süresini ayarladık. 2 saatlik süresi var resetPassword token kullandığımız için
        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromHours(2);
        });


        //Identity için aşağıdaki şekilde tanımlamalarımızı yaptık
        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); // default olarak 5 dakika kitlenir burada biz kitli kalma süresini değiştirdik
            opt.Lockout.MaxFailedAccessAttempts = 3; //başarısız olarak girilme teşebbüs sayısı 3 yaptık 

        }).AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders(); //AddDefaultTokenProvider : Şifremi unuttum methoduyla kullanıcıya sıfırlama mail gönderirken belirli bir
                                     //token üzerinden gönderiyoruzki mesela 2 saat içerisinde geçerli olsun 


        //Cookie ayarları
        services.ConfigureApplicationCookie(opt =>
        {
            opt.LoginPath = "/Login/SignIn/"; //burada yetkisiz kişinin nereye yönlendireceğini belirledik.

            var cookieBuilder = new CookieBuilder(); //Cookie ayarları için bir builder türettik.
            cookieBuilder.Name = "IdentityMailCookie"; //Cookie ismini belirledik.

            opt.Cookie = cookieBuilder; //Cookiemizin tanımladığımız cookieBuildera tanımladık.
            opt.ExpireTimeSpan = TimeSpan.FromDays(60); //cookiemizin yaşam süresini belirledik 60 gün olarak.
            opt.SlidingExpiration = true; //cookimizn her girişinde yaşam süresi uzatılsın mı diye soruyoruz. biz evet dedik
                                          //yukarıdaki expritetime mantığı : Kullanıcı giriş yaptı ve 60 gün boyunca cookie saklandı Okey. ancak 60 gün sonunda 
                                          //bize tekrar şifre soracak sliding olayı ise bu 60 günlük zaman zarfında girerse sayacı 0 dan başlatıyor 
                                          //örneğin kullanıcı 30. günde giriş yaptı artık 60 tekrar baştan başladı yani kullanıcı 30 + 60 = 90 gün şifresiz girebilir.


        }).AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);


        services.AddAuthentication().AddGoogle(opt =>
        {
            opt.ClientId = configuration["Authentication:Google:ClientID"] ;
            opt.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        });

        services.AddAuthentication().AddFacebook(opt =>
        {
            opt.AppId = configuration["Authentication:Facebook:AppID"];
            opt.AppSecret = configuration["Authentication:Facebook:AppSecret"];
        });


    }
}

