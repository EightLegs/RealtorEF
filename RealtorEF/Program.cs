using Microsoft.EntityFrameworkCore;


using RealtorEF.Data;
using RealtorEF.Entities;

namespace RealtorEF
{
    public class Programm
    {
        public static void Main(String[] args)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                int a = 5;
                int b = 6;
            }
        }
    }
}