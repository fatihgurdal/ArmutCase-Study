### Armut Case Çalışması

API 7 uç hazırlanmıştır. `User` ve `Message` talimatların kayıtları ve yapılan işlemlerin geçmişi kaydedilmektedir.Swagger üzerinden kısa açıklamalar mevcuttur. Tüm environmentları docker compose yml ile verilmesine dikkat edilmiştir.

- POST `/api/Account`
- POST `/api/Account/Login`
- GET `/api/Account/{id}`
- POST `/api/Account/Block`
- GET `/api/Account/Activities`
- POST `/api/Message`
- GET `/api/Message/{userName}`

## Hızlı kurulum için bağımlılıklar
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/downloads)

## Hızlı Kurulum
- `git clone https://github.com/fatihgurdal/ArmutCase-Study.git`

-  `cd ArmutCase-Study`

- `docker-compose up`

- http://localhost:12001/swagger adresinde swagger karşılar.


## Kullanılan Teknoloji ve Kütüphaneler
**Framework:** .NET Core 6.0 ile ASPNET Web Api

**Database:** Mongo DB

**MediatR:** [MediatR](https://www.nuget.org/packages/MediatR)

**Validation:** [FluentValidation](https://www.nuget.org/packages/FluentValidation.AspNetCore/)

**Dokümantasyon:** [Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/ "Swagger")

**Test:** NUnit ve [Ductus FluentDocker](https://www.nuget.org/packages/Ductus.FluentDocker) birlikte testlerin docker çalışması.

*Not*: Yoğun bir hafta geçirdiğim için ek süre talebimde yeterli olmamıştır. Bu sebepten dolayı tamamladığım kadarıyla göndermek istedim. Test tarafı hedeflediğimin çok altında kalmıştır.
