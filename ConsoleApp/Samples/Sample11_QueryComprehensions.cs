using System;
using System.Linq;
using ConsoleApp.Helpers;

namespace ConsoleApp.Samples
{
    // http://bartdesmet.net/blogs/bart/archive/2008/08/30/c-3-0-query-expression-translation-cheat-sheet.aspx
    // https://www.ecma-international.org/publications/files/ECMA-ST/ECMA-334.pdf (12.17.3 Query expression translation)
    internal static class Sample11_QueryComprehensions
    {
        public static void Run()
        {
//            Run1();
//            Run2();
//            Run3();
//            Run4();
//            Run5();
//            Run6();
        }

        private static void Run1()
        {
            var numbers = new[] { 15, -1, 8, 0, 3, 234, -7 };

            var result1 =
                from number in numbers
                where number > 0
                orderby number descending
                select number;

            result1.Dump();

            Program.BlankLine();

            var result2 =
                from number in numbers
                let abs = Math.Abs(number)
                where abs > 5
                orderby number
                select number;

            result2.Dump();
        }

        private static void Run2()
        {
            var data = new[]
            {
                new { Name = "Николай", Age = 25 },
                new { Name = "Андрей", Age = 20 },
                new { Name = "Андрей", Age = 30 },
                new { Name = "Николай", Age = 35 }
            };

            var result =
                from person in data
                orderby person.Name ascending, person.Age descending
                select $"{person.Name}, возраст: {person.Age} лет";

            result.Dump("Name asc, Age asc");
        }

        private static void Run3()
        {
            var persons = new[]
            {
                new { FirstName = "Иван", LastName = "Иванов" },
                new { FirstName = "Алексей", LastName = "Петров" },
                new { FirstName = "Иван", LastName = "Михайлов" },
                new { FirstName = "Иван", LastName = "Иванов" },
            };

            var namesakes =
                from person in persons
                group person by person.FirstName;

            namesakes.Dump("Тёзки по имени");

            Program.BlankLine();

            var namesakes2 =
                from person in persons
                group person by person.FirstName into g
                select new
                {
                    Name = g.Key,
                    LastNames = from person in g select person.LastName
                };

            namesakes2.Dump("Тёзки по имени 2");

            Program.BlankLine();

            var fullNamesakes =
                from person in persons
                group person by new { person.FirstName, person.LastName } into g
                select new
                {
                    Person = g.Key,
                    Count = g.Count()
                } into stats
                where stats.Count > 1
                select stats.Person;

            fullNamesakes.Dump("Полные тёзки");
        }

        private static void Run4()
        {
            var peoples = new[]
            {
                new { Name = "Андрей", Cars = new[] { "Toyota" } },
                new { Name = "Иван",   Cars = Array.Empty<string>() },
                new { Name = "Петр",   Cars = new[] { "Nissan", "Honda" } }
            };

            var result =
                from people in peoples
                from car in people.Cars
                select new { Owner = people.Name, Car = car };

            result.Dump();
        }

        private static void Run5()
        {
            var peoples = new[]
            {
                new { Id = 1, FirstName = "Андрей", LastName = "Андреев" },
                new { Id = 2, FirstName = "Иван",   LastName = "Иванов" },
                new { Id = 3, FirstName = "Петр",   LastName = "Петров" }
            };

            var cars = new[]
            {
                new { OwnerId = 1, Make = "Toyota", OwnerFirstName = "Андрей", OwnerLastName = "Андреев" },
                new { OwnerId = 3, Make = "Nissan", OwnerFirstName = "Петр",   OwnerLastName = "Петров" },
                new { OwnerId = 3, Make = "Honda" , OwnerFirstName = "Петр",   OwnerLastName = "Петров" }
            };

            var first =
                from owner in peoples
                join car in cars on owner.Id equals car.OwnerId
                select new
                {
                    Name = $"{owner.FirstName} {owner.LastName}",
                    car.Make
                };

            first.Dump();

            Program.BlankLine();

            var second =
                from owner in peoples
                join car in cars on new { owner.FirstName, owner.LastName } equals new { FirstName = car.OwnerFirstName, LastName = car.OwnerLastName }
                select new
                {
                    Name = $"{owner.FirstName} {owner.LastName}",
                    car.Make
                };

            second.Dump();
        }

        private static void Run6()
        {
            var peoples = new[]
            {
                new { Id = 1, FirstName = "Андрей", LastName = "Андреев" },
                new { Id = 2, FirstName = "Иван",   LastName = "Иванов" },
                new { Id = 3, FirstName = "Петр",   LastName = "Петров" }
            };

            var cars = new[]
            {
                new { OwnerId = 1, Make = "Toyota" },
                new { OwnerId = 3, Make = "Nissan" },
                new { OwnerId = 3, Make = "Honda" }
            };

            var result =
                from owner in peoples
                join car in cars on owner.Id equals car.OwnerId into g
                select new
                {
                    Name = $"{owner.FirstName} {owner.LastName}",
                    Cars = g.Select(car => car.Make)
                };

            result.Dump();
        }
    }
}