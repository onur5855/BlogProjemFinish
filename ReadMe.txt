
BLOG PROJESÝ - 5432 

*  Blank Solution olarak projemizi oluþtuduk , oluþturacaðýmýz her bir farkl katmaný farklý prje tipi olarak ekleyip devam edeceðiz.

*  Solution altýna ilk etapta sýnýflarý oluþturacaðýmýz MODEL katmanýný (class library) ekleyeceðiz.

*  MODELS => Entities klasörü açýlýr. Abstract ve Concrete klasörleri altýna uygulamada kullanacaðýmýz sýnýflar eklenir.

*  MODELS => Enums kalsörü => uygulamada kullnýlacak enum yapýlarý ayrýca depolanýr.

*  MODELS => EntityTypeConfiguration klasörü => uyguamada kullanýlan sýnýflarýn veritabanýndaki iliþkileri/tablolarý için gerekli konfigurasyonlar yapýlýr.

NOT => Configuration için efcore.sqlServer paketi gerekli olacak.
       Mapleme iþlemlerinde IDENTITY kütühanesinden destek alacaðýmýz için mic.extension.identity.strores paketine ihtiyacýmýz olarak.
       IFormFile propertysi için efcore.http.features paketi gerekli.

*  *  *  *  *  *  *  *  *  * 

2.DATA ACCESS LAYER ( DAL )

olarak adlandýracaðýmýz veriye eriþim katmanýnda bizi veriTabanýna baðlayacak olan Context sýnýfýmýýzý ve Repolarýmýzý yazacðýz. Bu yüzden Class Library olarak bu katmaný açmamýz yeterli olacaktýr.

*  Solution altýna Class Library Prpjesi ekledikten sonra

* DAL => Context klasörü altýna ProjecCotext sýnýfýný oluþturuyoruz.
* DAL => Repository klasörü => Abstract - Concrete - Interfaces olmak üzere repolarýmý da parçalayacaðým.

 NOT => Gerekiyorsa ki gerekecektir MODEL katmaný ihtiyaç anýnda referans verilebilir.
       Gerekli olan mic.efcore tools/sqlServer/aspnetcore.identity paketleri ihtiyaç anýnda yüklenebilir.
       Bu projede context sýnýfý ÝdentityDbContextten kalýtým alacaðý için DbSetlerde belirttiðimiz sýnýflardan çok daha fazlasý veritabaný tarafýnda oluþacaktýr.

*  *  *  *  *  *  *  *  *  *

3.Kullanýcý ile iletiþime geçeceðimiz web katmaný için (UÝ katmaný da denir) ASPNET CORE WEB APP (Model -View - Controller) projemizi açtýk.

Göç baþlatacaðýz bunun için önce appsettingJsona connectionStrigimizi yazalým ve StartUpda söyleyelim.

Göç aþamasýnda package manager console da : default proje: contextin olduðu proje + dizinde web katmaný seçili olmalý.

Göç için efcore.design ve tools paketini kullanýyoruz.

add-migration isim ve update-database yapýlýr.

* Areas klasörünü açalým. => add- area - mvc area - isim verilir ve alan açýlmýþ olur.
StartUp içindeki endPoint düzenlenmeli.
NOT !!!  => AREA içinde açýlan Controllera muhakkak [Area("isim")] attribute eklenmeli yoksa çalýþmaz.

* Projede identity kütüphanesi kullanýdýðý starUpda söylenmeli.

* Ýlk etapta kullanýcý kayýt etmek ve devamýnda içeri giriþ yapailmesi için area ya ulaþmadan globadeki mvc yi kullanacaðýz. Sonrasýnda login iþlemleri baþarýlý olan kullanýcýyý area içine almýþ olacaðýz.

* Controllerlarda kullandýðýmýz repolarýn soyut hallerini constructorda çaðýýldýðýnda concrete hallerinin gelmesi için startUp içinde gömülü metotlardan faydalanarak IOC prensibini yerine getirmiþ olacaðýz.

* Ýhtiyacýmýz olduðunda Mapper kütüphanesi için => AutoMapper + AutoMapper.Mic.DI ( startUpda mapper kütühanesi metotlarý kullanýraka yaptýrýlan mapleme iþlemleri için bir sýnýf açýp bunu startUpda söyleyeceðiz.)

* Kullanýcý fotoðraf yükleeme iþlemleri için => SixLabors.ImageSharp + SixLabors.ImageSharp.web