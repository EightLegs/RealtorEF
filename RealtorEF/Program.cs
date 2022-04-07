using Microsoft.EntityFrameworkCore;


using RealtorEF.Data;
using RealtorEF.Entities;

using Microsoft.Extensions.Configuration;

namespace RealtorEF
{
    public class Programm
    {
        public static void Main(String[] args)
        {

            //services.AddDbContext<SchoolContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                int a = 5;
                int b = 6;
            }
        }
    }
}