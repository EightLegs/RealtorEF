using Microsoft.EntityFrameworkCore;


using RealtorEF.Data;
using RealtorEF.Entities;

namespace RealtorEF
{
    public class Program
    {
        //Каталог Data - ApplicationContext - класс, с помощью которого осущетвляется подключение к SQL Server (строка подключения находится в файле appsettings.json
        //Каталог Data - DBInitializer - класс, который заполняет базу данных рандомными данными для тестирования функционала лабораторной работы
        //Каталог Entities - Классы, ставящиеся в соответствие таблицам базы данных
        //Каталог Migrations - Снапшоты базы данных, слепки состояния базы данных, созданные с целью исправления ошибок и отката к начальному заполнению
        //QueryFunction - статический класс реализующий функционал в соответствии с заданиями лабораторной работы
        public static void Main(String[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new ArgumentException("<N задания> (1-20) <аргументы>\n");
                //Запуск команды смотри в Properties/launchSettings.json
                //В файле представлены входные данные для каждого задания (1-20)
                //При получении аргументов команда switch передает управление соответствующему кейсу (в случае задачи Номер 11 управление будет передано case "11"
                //В Console.WriteLine указано описание задания. Запуск функционала осуществляется посредством функций статичного класса QueryFunction
                switch (args[0])
                {
                    case "1":
                        var task1_District = args[1];
                        var task1_MinAmount = Convert.ToInt32(args[2]);
                        var task1_MaxAmount = Convert.ToInt32(args[3]);
                        Console.WriteLine("Задание1\nВывести объекты недвижимости, расположенные в указанном районестоимостью «ОТ» и «ДО»\n");
                        QueryFunction.GetRealEstateInPriceRangeByDisctrict(task1_District, task1_MinAmount, task1_MaxAmount);
                        break;
                    case "2":
                        Console.WriteLine(String.Format("Задание 2\nВывести фамилии риэлтор, которые продали объекты недвижимости c {0} комнатой(ами)\n", args[1]));
                        var task2_roomCount = Convert.ToInt32(args[1]);
                        QueryFunction.GetRealtorNameBySaledRealEstate(task2_roomCount);
                        break;
                    case "3":
                        Console.WriteLine(String.Format("Задание 3\nВывести разницу между заявленной и продажной стоимостью объектов недвижимости, расположенных на {0} этаже\n", args[1]));
                        var task3_floor = Convert.ToInt32(args[1]);
                        QueryFunction.GetAmountDifferenceBySaledRealEstate(task3_floor);
                        break;
                    case "4":
                        Console.WriteLine(String.Format("Задание 4\nОпределить общую стоимость всех {0}-комнатных объектовнедвижимости, расположенных в указанном районе\n", args[2]));
                        var task4_district = args[1];
                        var task4_roomCount = Convert.ToInt32(args[2]);
                        QueryFunction.GetRealEstateAmountSumByDistrict(task4_district, task4_roomCount);
                        break;
                    case "5":
                        Console.WriteLine(String.Format("Задание 5\nОпределить максимальную и минимальную стоимости объекта недвижимости, проданного указанным риэлтором {0}\n", args[1]));
                        var task5_realtor = args[1];
                        QueryFunction.GetMaxAndMinSaleAmountByRealtor(task5_realtor);
                        break;
                    case "6":
                        Console.WriteLine(String.Format("Задание 6\nОпределить среднюю оценку объектов недвижимости, расположенных вуказанном районе {0}\n", args[1]));
                        var task6_district = args[1];
                        QueryFunction.GetAverangeEvaluationByDistrict(task6_district);
                        break;
                    case "7":
                        Console.WriteLine(String.Format("Задание 7\nВывести информацию о количестве объектов недвижимости,расположенных на {0} этаже по каждому району\n", args[1]));
                        var task7_floor = Convert.ToInt32(args[1]);
                        QueryFunction.GetSecondFloorRealEstateCountByDictricts(task7_floor);
                        break;
                    case "8":
                        Console.WriteLine(String.Format("Задание 8\nОпределить среднюю оценку апартаментов типа {0} по критерию «Безопасность» {1},проданных указанным риэлтором {2}\n", args[1], args[2], args[3]));
                        var task8_type = Convert.ToInt32(args[1]);
                        var task8_criteriaId = Convert.ToInt32(args[2]);
                        var task8_realtor = args[3];
                        QueryFunction.GetAverangeCriteriaEvaluationByRealtor(task8_type, task8_criteriaId, task8_realtor);
                        break;
                    case "9":
                        Console.WriteLine(String.Format("Задание 9\nОпределить среднюю продажную стоимость 1м2 для квартир, которые были проданы в указанную дату «ОТ» и «ДО {0} - {1} (тип - {2})» \n", args[1], args[2],args[3]));
                        var task9_dt1 = DateTime.Parse(args[1]);
                        var task9_dt2 = DateTime.Parse(args[2]);
                        var task9_type = Convert.ToInt32(args[3]);
                        QueryFunction.GetAverangeAmountOfOneRealEstateSqMeterSaledBetween(task9_dt1, task9_dt1, task9_type);
                        break;
                    case "10":
                        Console.WriteLine(String.Format("Задание 10\nВывести информацию о премии риэлтора {0}, которая рассчитывается по формуле: Количество проданных квартир* Стоимость*5 % -НДФЛ(13 %)\n", args[1]));
                        var task10_realtor = args[1];
                        QueryFunction.GetRealtorBounty(task10_realtor);
                        break;
                    case "11":
                        Console.WriteLine(String.Format("Задание 11\nВывести информацию о количестве квартир типа {0}, проданных каждым риэлтором\n", args[1]));
                        var task11_type = Convert.ToInt32(args[1]);
                        QueryFunction.GetCountOfRealtorsSales(task11_type);
                        break;
                    case "12":
                        Console.WriteLine(String.Format("Задание 12\nВывести информацию о средней стоимости объектов недвижимости,расположенных на {0} этаже по каждому материалу здания\n", args[1]));
                        var task12_floor = Convert.ToInt32(args[1]);
                        QueryFunction.GetRealEstateAverangeAmountByMaterials(task12_floor);
                        break;
                    case "13":
                        Console.WriteLine("Задание 13\nВывести информацию о трех самых дорогих объектах недвижимости,расположенных в каждом районе.\n");
                        QueryFunction.GetMostExpensiveRealEstateByDistricts();
                        break;
                    case "14":
                        Console.WriteLine(String.Format("Задание 14\nОпределить адреса квартир, расположенных в указанном районе {0}, которые еще не проданы.\n", args[1]));
                        var task14_district = args[1];
                        QueryFunction.GetNotSoldRealEstateByDistrict(task14_district);
                        break;
                    case "15":
                        Console.WriteLine(String.Format("Задание 15\nВывести информацию об объектах недвижимости, у которых разница между заявленной и продажной стоимостью составляет не более {0} % ирасположенных в указанном районе {1}\n", args[1],args[2]));
                        var task15_percentage = Convert.ToInt32(args[1]);
                        var task15_district = args[2];
                        QueryFunction.GetRealEstateInDistrictByDifferencePercent(task15_percentage,task15_district);
                        break;
                    case "16":
                        Console.WriteLine(String.Format("Задание 16\nВывести информацию об объектах недвижимости, у которых разницамежду заявленной и продажной стоимостью составляет больше 100000 рублей ипроданную указанным риэлтором {0}\n", args[1]));
                        var task16_realtor = args[1];
                        QueryFunction.GetRealEstateAmountDifferenceByRealtor(task16_realtor);
                        break;
                    case "17":
                        Console.WriteLine(String.Format("Задание 17\nВывести разницу в % между заявленной и продажной стоимостью для объектов недвижимости, проданных указанным риэлтором {0} в текущем году\n", args[1]));
                        var task17_realtor = args[1];
                        QueryFunction.GetRealEstateAmountDifferenceByRealtor(task17_realtor);
                        break;
                    case "18":
                        Console.WriteLine("Определить адреса квартир, стоимость 1м2которых меньше средней порайону.");
                        QueryFunction.GetCheapestSqMeterRealEstateOfDistrict();
                        break;
                    case "19":
                        Console.WriteLine("Определить ФИО риэлторов, которые ничего не продали в текущем году.");
                        QueryFunction.GetRealtorsWheSaledNothingInCurrentYear();
                        break;
                    case "20":
                        Console.WriteLine("Вывести адреса объектов недвижимости, стоимость 1м2 которых меньшесредней всех объектов недвижимости по району, объявления о которых былиразмещены не более 4 месяцев назад");
                        QueryFunction.GetCheapestSqMeterRealEstateOfDistrictWhichRecentlyPosted();
                        break;
                    default:
                        Console.WriteLine(String.Format("Задание {0} не найдено", args[0]));
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private enum RealEstateTypes
        {
            Appartaments = 1,
            Flat = 2,
            _ = 3
        }
    }
}




/*            using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                
            }*/