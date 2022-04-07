using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using RealtorEF.Entities;

namespace RealtorEF.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            //DbInitializer.Initialize(this);
        }

        public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<EvaluationCriteria> EvaluationCriteria { get; set; }
        public DbSet<RealEstate> RealEstate { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            //DbInitializer.Initialize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=realtorefdb;Trusted_Connection=True;");
        }
    }
}
