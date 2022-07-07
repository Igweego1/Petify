using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petify.WebApi.Data;
using Petify.WebApi.Model;

namespace Petify.WebApi.Data_Model
{
    public class PetifyDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public PetifyDbContext(DbContextOptions<PetifyDbContext> options) : base(options)
        {

        }


        public DbSet<Pet> Pets { get; set; } = null!;

        public DbSet<UserImage> UserImages { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<SubCategory> SubCategories { get; set; } = null!;

        public DbSet<Allergies> Allergies { get; set; } = null!;

        public DbSet<PetAllergy> PetsAllergy { get; set; } = null!;

        public DbSet<Services> Services { get; set; } = null!;

        public DbSet<PetServices> PetServices { get; set; } = null!;

        public DbSet<CheckInCheckOut> CheckInCheckOut { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;

        public DbSet<Billing> Billing { get; set; } = null!;

        public DbSet<FeedBack> FeedBack { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
