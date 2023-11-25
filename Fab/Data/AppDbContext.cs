using Fab.Models.AboutFolder;
using Fab.Models.AchievementFolder;
using Fab.Models.ActivitiyFolder;
using Fab.Models.AdsFolder;
using Fab.Models.BannersFolder;
using Fab.Models.BlogsFolder;
using Fab.Models.CategoriesFolder;
using Fab.Models.ContactFolder;
using Fab.Models.CorporateFolder;
using Fab.Models.CVFolder;

using Fab.Models.Gornushler;
using Fab.Models.HumanResources;
using Fab.Models.InteriorFolder;
using Fab.Models.NewsFolder;
using Fab.Models.PressFolder;
using Fab.Models.ProductFolder;
using Fab.Models.SubcategoryFolder;
using Fab.Models.TetbiqSaheleriFolder;

using Fab.Models.TwinFolder;
using Fab.Models.UserFolder;
using Fab.Models.VacanciesFolder;
using Fab.Models.VisionFolder;
using Fab.Models.Xususiyyetler;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fab.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Ads> Ads { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerTranslate> BannerTranslates { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTranslate> BlogTranslates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslate> CategoryTranslates { get; set; }
        public DbSet<Corporate> Corporates { get; set; }
        public DbSet<CorporateTranslate> CorporateTranslates { get; set; }
        public DbSet<Interior> Interiors { get; set; }
        public DbSet<InteriorTranslate> InteriorTranslates { get; set; }
        public DbSet<News> News  { get; set; }
        public DbSet<NewsTranslate> NewsTranslates  { get; set; }
        public DbSet<Twin> Twins { get; set; }
        public DbSet<TwinTranslate> TwinTranslates { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyTranslate> VacancyTranslates { get; set; }
        public DbSet<Press> Presses { get; set; }
        public DbSet<PressTranslate> PressTranslates { get; set; }
        public DbSet<HumanResource> HumanResources { get; set; }
        public DbSet<HumanResourceTranslate> HumanResourceTranslates { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<SubcategoryTranslate> SubcategoryTranslates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslate> ProductTranslates { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInformations> ContactInformations { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Achivement> Achivements { get; set; }
        public DbSet<Vision> Visions { get; set; }
        public DbSet<VisionTranslate> VisionTranslates { get; set; }
        public DbSet<AboutTranslate> AboutTranslates { get; set; }
        public DbSet<AchivementTranslate> AchivementTranslates { get; set; }
        public DbSet<ActivityTranslate> ActivityTranslates { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<AppearanceField> AppearanceFields { get; set; }
        public DbSet<AppearanceFIeldTranslate> AppearanceFIeldTranslates { get; set; }
        public DbSet<ApplicationField> ApplicationFields { get; set; }
        public DbSet<ApplicationFieldTranslate> ApplicationFieldTranslates { get; set; }
        public DbSet<Characteristics> Characteristics { get; set; }
        public DbSet<CharacteristicsTranslate> CharacteristicsTranslates { get; set; }



    }
}
