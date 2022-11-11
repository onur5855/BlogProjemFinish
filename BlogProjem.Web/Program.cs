using BlogProjem.Dal.Context;
using BlogProjem.Dal.Repositories.Concrete;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Models.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ProjectContext>();

//Cookie için kod  yazýlacak
builder.Services.ConfigureApplicationCookie(a => {

    a.LoginPath = new PathString("/Home/Login");//cokide kimse yoksa gideceði sayfa yoksa
    a.AccessDeniedPath = "/Home/Login"; //cokideki kiþi admin sayfasýna giriþ yapmaya yetkisizse gideceði yer
    a.ExpireTimeSpan=TimeSpan.FromDays(1);//kaç gün kalacak  cookiede kalacak
    a.SlidingExpiration = true;  // içerdekii son  hareketten sonra cookie süresi , bir daha cookie süresi kadar uzatulsýn mý, - evet demiþ olduk.
    a.Cookie = new CookieBuilder { Name = "UserCookie", SecurePolicy = CookieSecurePolicy.Always };

});





builder.Services.AddAuthentication();  // kimlik doðrulama

builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
builder.Services.AddScoped<IUserFollowCategoryRepository, UserFollowCategoryRepository>();//eklendi
builder.Services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();//eklendi

builder.Services.AddAutoMapper(typeof(Mapping));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();  // kimlik doðrulama
app.UseAuthorization();  // yetkilendirme


app.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "Areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
