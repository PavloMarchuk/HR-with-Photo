using HR.DateLayer.DbLayer;
using HR.DateLayer.Repositories;
using HR.TestConnection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HR.TestConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            //встановлення шляху до поточного каталогку
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //отримуэмо конфігурацію із файла "appsettings.json"
            builder.AddJsonFile("appsettings.json");
            //створюємо конфігурацію
            var config = builder.Build();
            //створюєм строку підключення
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<HumanResourcesContext>();
            DbContextOptions options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            IGenericRepository<Employee> repository = new EmployeeRepository(new HumanResourcesContext(options));

            EmployeeFilter filter = new EmployeeFilter()
            {
                //FirstName = "I",
                //LastName = "a"
                StartBirthday = new DateTime(1989,01,01),
                EndBirthday = new DateTime(1996, 01, 01)
            };

            var collection = repository.FindBy(filter.predicate);

            foreach (var item in collection)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName} {item.DateBirthday:d}");
            }







            //using (var context = new HumanResourcesContext(options))
            //{
            //    var model = context.Employee.ToArrayAsync();
            //    model.Wait();
            //    var collection = model.Result;

            //    foreach (var item in collection)
            //    {
            //        Console.WriteLine($"{item.FirstName} {item.LastName} {item.DateBirthday:d}");
            //    }
            //}
        }
    }
}
