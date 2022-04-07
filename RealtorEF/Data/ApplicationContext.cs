using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using RealtorEF.Entities;

namespace RealtorEF.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            DbInitializer.Initialize(this);
        }

        public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<EvaluationCriteria> EvaluationCriteria { get; set; }
        public DbSet<RealEstate> RealEstate { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Получение строки подключения из файла appsettings.json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
