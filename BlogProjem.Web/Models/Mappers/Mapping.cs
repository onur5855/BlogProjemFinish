using AutoMapper;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.VMs;
using BlogProjem.Web.Models.DTOs;

namespace BlogProjem.Web.Models.Mappers
{
    public class Mapping:Profile
    {
        // mapper kütüphanesiin yapmasını istediğimiz mapleme işlemlerini tek tek burada söylememiz gerekiyor.

        // bu sınıfı oluşturdul çünkü startUpda bize mapleme işlemlerini nerede yapıyorsun diye soracaktı.

        public Mapping()
        {
            CreateMap<CreateUserDTO, AppUser>().ReverseMap(); // reverseMap : mapleme işlemini iki yönlü çalıştır.
            CreateMap<CreateCategoryDTO, Category>();// dtodan gelenleri categoriye atiyor
            CreateMap<CreateArticleVM,Article>();
            CreateMap<ArticleCategoryDTO,ArticleCategory>();
            CreateMap<UpdateArticleVm,Article>();
            CreateMap<UpdateCategoryDTO,Category>();
            CreateMap<FollowDTO, UserFollowCategory>();
            


        }


    }
}
