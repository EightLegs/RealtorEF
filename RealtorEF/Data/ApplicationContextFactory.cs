using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.Extensions.Configuration;

namespace RealtorEF.Data
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            //Получение строки подключения из файла appsettings.json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            string connectionString = config.GetSection("DefaultConnection")["ConnectionString"];

            optionsBuilder.UseSqlServer(connectionString);
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
