using Microsoft.EntityFrameworkCore;
using RealtorEF.Data;
using RealtorEF.Entities;

namespace RealtorEF
{
    public static class QueryFunction
    {
        //Задание 1
        public static void GetRealEstateInPriceRangeByDisctrict(string district, int minAmount, int maxAmount)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var realEstate in db.RealEstate.Where(r => r.District.Name == district
                                                                    && r.Price >= minAmount
                                                                    && r.Price <= maxAmount)
                )
                {
                    Console.WriteLine(realEstate.Address
                        + ", " + realEstate.Square.ToString()
                        + ", " + realEstate.Floor.ToString());
                }
            }
        }
        //Задание 2
        public static void GetRealtorNameBySaledRealEstate(int _roomCount)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var people = new List<Realtor>();
                foreach (var sale in db.Sales.Include("Realtor").Where(s => s.RealEstate.RoomCount == _roomCount))
                {
                    if (people.Contains(sale.Realtor))
                    {
                        continue;
                    }

                    Console.WriteLine(sale.Realtor.LastName + " " + sale.Realtor.FirstName + " " + sale.Realtor.MiddleName);
                    people.Add(sale.Realtor);
                }
            }
        }
        //Задание 3
        public static void GetAmountDifferenceBySaledRealEstate(int _floor)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var sale in db
                    .Sales
                    .Include("RealEstate")
                    .Include("Realtor")
                    .Where(s => s.RealEstate.Floor == _floor)
                )
                {
                    var difference = Math.Abs(sale.Price - sale.RealEstate.Price);
                    Console.WriteLine(sale.RealEstate.Address + " "
                        + difference.ToString() + " "
                        + sale.Realtor.LastName + " "
                        + sale.Realtor.FirstName + " "
                        + sale.Realtor.MiddleName);
                }
            }
        }
        //Задание 4
        public static void GetRealEstateAmountSumByDistrict(string district, int _roomCount)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                int sum = 0;
                foreach (var realEstate in db
                    .RealEstate
                    .Include("District")
                    .Where(r => r.RoomCount == _roomCount && r.District.Name == district)
                )
                {
                    sum += realEstate.Price;
                }
                Console.WriteLine(sum);
            }
        }
        //Задание 5
        public static void GetMaxAndMinSaleAmountByRealtor(string lastName)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var sales = db.Sales.Include("Realtor").Where(s => s.Realtor.LastName == lastName);
                Console.WriteLine(sales.Max(s => s.Price).ToString() + " " + sales.Min(s => s.Price).ToString());
            }
        }
        //Задание 6
        public static void GetAverangeEvaluationByDistrict(string district)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var averange = db
                    .Evaluations
                    .Include("RealEstate")
                    .Where(e => e.RealEstate.District.Name == district)
                    .Average(e => e.Value);
                Console.WriteLine(averange);
            }
        }
        //Задание 7
        public static void GetSecondFloorRealEstateCountByDictricts(int _floor)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var district in db.Districts)
                {
                    var realEstateCount = db
                        .RealEstate.Include("District")
                        .Where(r => r.District.Id == district.Id && r.Floor == _floor)
                        .Count();
                    Console.WriteLine(district.Name + " " + realEstateCount.ToString());
                }
            }
        }
        //Задание 8
        public static void GetAverangeCriteriaEvaluationByRealtor( int _realEstateType,int _criteriaId, string lastName)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var sales = db
                    .Sales
                    .Include("RealEstate")
                    .Include("Realtor")
                    .Where(s => s.Realtor.LastName == lastName && s.RealEstate.Type == _realEstateType);

                var evaluations = db
                    .Evaluations
                    .Where(e => sales.Any(s => s.RealEstateId == e.RealEstateId) && e.EvaluationCriteriaId == _criteriaId);

                if (evaluations.Count() == 0)
                {
                    Console.WriteLine("Nothing found");
                    return;
                }

                Console.WriteLine(evaluations.Average(e => e.Value));
            }
        }
        //Задание 9
        public static void GetAverangeAmountOfOneRealEstateSqMeterSaledBetween(DateTime since, DateTime before, int _realEstateFlatType)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var sales = db
                    .Sales
                    .Include("RealEstate")
                    .Include("Realtor")
                    .Where(s => s.RealEstate.Type == _realEstateFlatType)
                    .Where(s => s.Date >= since)
                    .Where(s => s.Date <= before);


                if (sales.Count() == 0)
                {
                    Console.WriteLine("Nothing found");
                    return;
                }

                Console.WriteLine(sales.Average(s => (s.Price / s.RealEstate.Square)));
            }
        }
        //Задание 10
        public static void GetRealtorBounty(string lastName)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var realtor = db.Realtors.Where(r => r.LastName == lastName).FirstOrDefault();

                if (realtor != null)
                {
                    var sales = db.Sales.Include("Realtor").Where(s => s.RealtorId == realtor.Id);
                    var salesCount = sales.Count();
                    var salesAmountSum = sales.Sum(s => s.Price);

                    var bounty = (salesCount * salesAmountSum * 0.05) - ((salesCount * salesAmountSum * 0.05) * 0.13);

                    var result = string.Format("{0} {1} {2} - {3}", realtor.LastName, realtor.FirstName, realtor.MiddleName, bounty);
                    Console.WriteLine(result);
                    return;
                }
                Console.WriteLine("Realtor not found");
            }
        }
        //Задание 11
        public static void GetCountOfRealtorsSales(int _realEstateFlatType)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var sales = db
                    .Sales
                    .Include("Realtor")
                    .Include("RealEstate")
                    .Where(s => s.RealEstate.Type == _realEstateFlatType);

                foreach (var realtor in db.Realtors)
                {
                    Console.WriteLine(string.Format("{0} {1} {2} - {3}",
                        realtor.LastName,
                        realtor.FirstName,
                        realtor.MiddleName,
                        sales.Where(s => s.RealtorId == realtor.Id).Count()
                    ));
                }
            }
        }
        //Задание 12
        public static void GetRealEstateAverangeAmountByMaterials(int _floor)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var materials = db.BuildingMaterials;

                foreach (var material in materials)
                {
                    var realEstate = db
                        .RealEstate
                        .Where(r => r.Floor == _floor)
                        .Where(r => r.BuildingMaterialId == material.Id);

                    var averange = 0.0;
                    if (realEstate.Count() != 0)
                    {
                        averange = realEstate.Average(r => r.Price);
                    }

                    Console.WriteLine(material.Name + " " + averange.ToString());
                }
            }
        }

        //Задание 13
        public static void GetMostExpensiveRealEstateByDistricts()
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var district in db.Districts)
                {
                    var realEstate = db
                        .RealEstate
                        .Include("District")
                        .Where(r => r.DistrictId == district.Id)
                        .OrderByDescending(r => r.Price)
                        .ThenBy(r => r.Floor)
                        .Take(3);

                    foreach (var singleRealEstate in realEstate)
                    {
                        var row = string.Format("{0} - {1} {2} {3}",
                            singleRealEstate.District.Name,
                            singleRealEstate.Address,
                            singleRealEstate.Price,
                            singleRealEstate.Floor);
                        Console.WriteLine(row);
                    }
                }
            }
        }
        //Задание 14
        public static void GetNotSoldRealEstateByDistrict(string district)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var realEstate in db
                    .RealEstate
                    .Include("District")
                    .Where(r => r.District.Name == district
                    && r.Status == 1)
                )
                {
                    Console.WriteLine(realEstate.Address);
                }
            }
        }

        //Задание 15
        public static void GetRealEstateInDistrictByDifferencePercent( int _percentageDifference, string district)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var districtEntity = db.Districts.Where(d => d.Name == district).FirstOrDefault();
                foreach (var realEstate in db.RealEstate.Where(r => r.DistrictId == districtEntity.Id && r.Status == 0))
                {
                    var requestAmount = realEstate.Price;
                    var saleAmount = db.Sales.Where(s => s.RealEstateId == realEstate.Id).FirstOrDefault().Price;

                    var percentageDifference = (requestAmount * 100) / saleAmount;
                    if (percentageDifference <= _percentageDifference)
                    {
                        Console.WriteLine(realEstate.Address + " " + districtEntity.Name);
                    }
                }
            }
        }
        //Задание 16
        public static void GetRealEstateAmountDifferenceByRealtor(string lastName)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var realtor =db.Realtors.Where(r => r.LastName == lastName).FirstOrDefault();
                foreach (var sale in db.Sales.Include("RealEstate").Where(s => s.RealtorId == realtor.Id))
                {
                    var requestAmount = sale.RealEstate.Price;
                    var saleAmount = sale.Price;

                    if (Math.Abs(requestAmount - saleAmount) >= 100000)
                    {
                        var district = db.Districts.Where(d => d.Id == sale.RealEstate.DistrictId).FirstOrDefault();
                        Console.WriteLine(sale.RealEstate.Address + " " + district.Name);
                    }
                }
            }
        }
        //Задание 17

        public static void GetRealEstateAmountDifferenceByRealtorAndYear(string lastName, int year)
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                var realtor = db.Realtors.Where(r => r.LastName == lastName).FirstOrDefault();
                foreach (var sale in db
                    .Sales
                    .Include("RealEstate")
                    .Where(s => s.RealtorId == realtor.Id)
                    .Where(s => s.Date.Year == year)
                )
                {
                    var requestAmount = sale.RealEstate.Price;
                    var saleAmount = sale.Price;

                    var percentageDifference = (requestAmount * 100) / saleAmount;

                    Console.WriteLine(sale.RealEstate.Address + " " + percentageDifference.ToString());
                }
            }
        }
        //Задание 18
        public static void GetCheapestSqMeterRealEstateOfDistrict()
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var district in db.Districts)
                {
                    if (db.RealEstate.Where(r => r.DistrictId == district.Id).Count() == 0)
                    {
                        continue;
                    }

                    var districtAverangeSqMeterAmount = Convert.ToInt32(
                        db
                        .RealEstate
                        .Where(r => r.DistrictId == district.Id)
                        .Average(r => (r.Price / r.Square))
                    );
                    foreach (var realEstate in db.RealEstate.Where(r => r.DistrictId == district.Id))
                    {
                        var realEstateAverangeSqMeterAmount = realEstate.Price / realEstate.Square;
                        if (realEstateAverangeSqMeterAmount <= districtAverangeSqMeterAmount)
                        {
                            Console.WriteLine(realEstate.Address);
                        }
                    }
                }
            }
        }

        //Задание 19
        public static void GetRealtorsWheSaledNothingInCurrentYear()
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var realtor in db.Realtors)
                {
                    if (!db.Sales.Where(s => s.Date.Year == DateTime.Now.Year).Any(s => s.RealtorId == realtor.Id))
                    {
                        Console.WriteLine(realtor.LastName + " " + realtor.FirstName + " " + realtor.MiddleName);
                    }
                }
            }
        }
        //Задание 20
        public static void GetCheapestSqMeterRealEstateOfDistrictWhichRecentlyPosted()
        {
            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                foreach (var district in db.Districts)
                {
                    if (db.RealEstate.Where(r => r.DistrictId == district.Id).Count() == 0)
                    {
                        continue;
                    }

                    var districtAverangeSqMeterAmount = Convert.ToInt32(
                        db
                        .RealEstate
                        .Where(r => r.DistrictId == district.Id)
                        .Average(r => (r.Price / r.Square))
                    );
                    foreach (var realEstate in db.RealEstate.Where(r => r.DistrictId == district.Id))
                    {
                        var realEstateAverangeSqMeterAmount = realEstate.Price / realEstate.Square;
                        if (realEstateAverangeSqMeterAmount <= districtAverangeSqMeterAmount
                            && GetMonthDateDifference(realEstate.Date, DateTime.Now) < 4)
                        {
                            Console.WriteLine(realEstate.Address);
                        }
                    }
                }
            }
        }
        private static int GetMonthDateDifference(DateTime since, DateTime before)
        {
            if (before < since)
            {
                return -1;
            }

            var monthes = 0;

            while ((before.Month < since.Month) && (before.Year > since.Year))
            {
                since = since.AddMonths(1);
                monthes++;
            }

            return monthes;
        }
    }
}
