
BLOG PROJES� - 5432 

*  Blank Solution olarak projemizi olu�tuduk , olu�turaca��m�z her bir farkl katman� farkl� prje tipi olarak ekleyip devam edece�iz.

*  Solution alt�na ilk etapta s�n�flar� olu�turaca��m�z MODEL katman�n� (class library) ekleyece�iz.

*  MODELS => Entities klas�r� a��l�r. Abstract ve Concrete klas�rleri alt�na uygulamada kullanaca��m�z s�n�flar eklenir.

*  MODELS => Enums kals�r� => uygulamada kulln�lacak enum yap�lar� ayr�ca depolan�r.

*  MODELS => EntityTypeConfiguration klas�r� => uyguamada kullan�lan s�n�flar�n veritaban�ndaki ili�kileri/tablolar� i�in gerekli konfigurasyonlar yap�l�r.

NOT => Configuration i�in efcore.sqlServer paketi gerekli olacak.
       Mapleme i�lemlerinde IDENTITY k�t�hanesinden destek alaca��m�z i�in mic.extension.identity.strores paketine ihtiyac�m�z olarak.
       IFormFile propertysi i�in efcore.http.features paketi gerekli.

*  *  *  *  *  *  *  *  *  * 

2.DATA ACCESS LAYER ( DAL )

olarak adland�raca��m�z veriye eri�im katman�nda bizi veriTaban�na ba�layacak olan Context s�n�f�m��z� ve Repolar�m�z� yazac��z. Bu y�zden Class Library olarak bu katman� a�mam�z yeterli olacakt�r.

*  Solution alt�na Class Library Prpjesi ekledikten sonra

* DAL => Context klas�r� alt�na ProjecCotext s�n�f�n� olu�turuyoruz.
* DAL => Repository klas�r� => Abstract - Concrete - Interfaces olmak �zere repolar�m� da par�alayaca��m.

 NOT => Gerekiyorsa ki gerekecektir MODEL katman� ihtiya� an�nda referans verilebilir.
       Gerekli olan mic.efcore tools/sqlServer/aspnetcore.identity paketleri ihtiya� an�nda y�klenebilir.
       Bu projede context s�n�f� �dentityDbContextten kal�t�m alaca�� i�in DbSetlerde belirtti�imiz s�n�flardan �ok daha fazlas� veritaban� taraf�nda olu�acakt�r.

*  *  *  *  *  *  *  *  *  *

3.Kullan�c� ile ileti�ime ge�ece�imiz web katman� i�in (U� katman� da denir) ASPNET CORE WEB APP (Model -View - Controller) projemizi a�t�k.

G�� ba�lataca��z bunun i�in �nce appsettingJsona connectionStrigimizi yazal�m ve StartUpda s�yleyelim.

G�� a�amas�nda package manager console da : default proje: contextin oldu�u proje + dizinde web katman� se�ili olmal�.

G�� i�in efcore.design ve tools paketini kullan�yoruz.

add-migration isim ve update-database yap�l�r.

* Areas klas�r�n� a�al�m. => add- area - mvc area - isim verilir ve alan a��lm�� olur.
StartUp i�indeki endPoint d�zenlenmeli.
NOT !!!  => AREA i�inde a��lan Controllera muhakkak [Area("isim")] attribute eklenmeli yoksa �al��maz.

* Projede identity k�t�phanesi kullan�d��� starUpda s�ylenmeli.

* �lk etapta kullan�c� kay�t etmek ve devam�nda i�eri giri� yapailmesi i�in area ya ula�madan globadeki mvc yi kullanaca��z. Sonras�nda login i�lemleri ba�ar�l� olan kullan�c�y� area i�ine alm�� olaca��z.

* Controllerlarda kulland���m�z repolar�n soyut hallerini constructorda �a���ld���nda concrete hallerinin gelmesi i�in startUp i�inde g�m�l� metotlardan faydalanarak IOC prensibini yerine getirmi� olaca��z.

* �htiyac�m�z oldu�unda Mapper k�t�phanesi i�in => AutoMapper + AutoMapper.Mic.DI ( startUpda mapper k�t�hanesi metotlar� kullan�raka yapt�r�lan mapleme i�lemleri i�in bir s�n�f a��p bunu startUpda s�yleyece�iz.)

* Kullan�c� foto�raf y�kleeme i�lemleri i�in => SixLabors.ImageSharp + SixLabors.ImageSharp.web