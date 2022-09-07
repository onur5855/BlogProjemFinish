using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Model.EntityTypeConfigurations.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Context
{
    // DbContextten değil IdentityDbContextten kalıtım aldık çünkü projede idetity kütüphanesini kullanacağız.
    public class ProjectContext : IdentityDbContext
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<UserFollowCategory> FollowCategories { get; set; } 
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<UserPassword> userPasswords { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleMap());
            builder.ApplyConfiguration(new AppUserMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new CommentMap());
            builder.ApplyConfiguration(new LikeMap());
            builder.ApplyConfiguration(new IdentityRoleMap());
            builder.ApplyConfiguration(new UserFollowedCategoryMap());
            builder.ApplyConfiguration(new ArticleCategoryMap());
            builder.ApplyConfiguration(new UserPasswordMap()); 

            base.OnModelCreating(builder);
        }




    }
}
