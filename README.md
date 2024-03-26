# IdentityMessage

Bu proje, kullanıcı kimlik doğrulaması ve yetkilendirme işlemlerini yönetmek için geliştirilmiş bir web uygulamasıdır. .NET 6 kullanılarak MVC mimarisiyle oluşturulmuştur.

## Kullanılan Teknolojiler

- **ASP.NET 6 MVC:** Web uygulamaları geliştirmek için kullanılan bir framework.
- **Entity Framework Core:** Veritabanı işlemlerini yönetmek için kullanılan bir ORM aracı.
- **Fluent Validation:** Veri doğrulamasını gerçekleştirmek için kullanılan bir kütüphane.
- **Google ve Facebook Girişi:** Kullanıcıların Google veya Facebook hesapları ile giriş yapmalarını sağlayan entegrasyonlar.
- **E-posta Gönderme:** Şifre sıfırlama bağlantılarını e-posta ile kullanıcılara göndermek için SMTP servislerini kullanma.

## Proje Detayları

Proje, aşağıdaki işlevselliklere sahiptir:

### Kimlik Doğrulama ve Yetkilendirme

- Kullanıcı Kaydı: Yeni kullanıcıların kaydının yapılması.
- Kullanıcı Girişi: Kayıtlı kullanıcıların sisteme giriş yapması.
- Parola Sıfırlama: Kullanıcıların şifrelerini unuttuklarında parola sıfırlama işlemi.
- Şifre Değiştirme: Kullanıcıların hesap ayarlarından şifrelerini değiştirmesi.
- Yetkilendirme: Kullanıcıların sadece yetkilerine uygun işlemleri yapabilmesi için yetkilendirme.

### Veri Doğrulama

- Fluent Validation kullanılarak gelen verilerin doğruluğunun sağlanması.
- Hata Mesajları: Doğrulama hataları için kullanıcı dostu hata mesajlarının gösterilmesi.

### Entegrasyonlar

- Google ve Facebook Girişi: Kullanıcıların Google veya Facebook hesapları ile giriş yapmalarını sağlayan entegrasyonlar.
- SMTP Servisleri: Şifre sıfırlama bağlantılarının e-posta ile kullanıcılara gönderilmesi için SMTP servislerinin kullanılması.

## Kurulum

Gerekli Araçlar ve Ortam:
- .NET 6 SDK ve Visual Studio gibi geliştirme ortamlarının kurulması.
- Veritabanı olarak SQL Server'ın kurulması veya uzak bir SQL Server bağlantısının sağlanması.

Projenin İndirilmesi ve Çalıştırılması:
- Projenin GitHub deposundan veya ZIP olarak indirilmesi.
- Visual Studio'da projenin açılması.
- Veritabanının oluşturulması ve gerekli bağlantı ayarlarının yapılması.
- Projeyi çalıştırarak web sunucusunda uygulamanın başlatılması.

## Ekran Görüntüleri

Aşağıda, projenin bazı ekran görüntüleri bulunmaktadır:
(Projenin ekran görüntüleri buraya eklenebilir)

--- 

Bu dokümantasyon, Identity projesinin kullanılan teknolojilerini, işlevselliğini ve kurulumunu açıklar. Projeye daha fazla detay eklemek veya özelleştirmek isterseniz, gerektiği gibi düzenleyebilirsiniz.
