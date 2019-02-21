using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    // https://linqsamples.com/
    internal static class Sample9_StandardOperators
    {
        public static void Run()
        {
//            Run1();
//            Run2();
//            Run3();
//            Run4();
//            Run5();
//            Run6();
//            Run7();
//            Run8();
//            Run9();
//            Run10();
//            Run11();
//            Run12();
//            Run13();
//            Run14();
//            Run15();
//            Run16();
//            Run17();
//            Run18();
//            Run19();
//            Run20();
//            Run21();
//            Run22();
//            Run23();
//            Run24();
//            Run25();
            Run26();
//            Run27();
        }

        #region Empty
        private static void Run1()
        {
            Enumerable.Empty<int>().Dump();
        }
        #endregion

        #region Return (Single item)
        private static IEnumerable<T> Return<T>(T item)
        {
            yield return item;

            // return new[] { item };
            // return new List<T>{ item };
        }

        private static void Run2()
        {
            Return(42).Dump();
        }
        #endregion

        #region Ana (Генерация)
        private static void Run3()
        {
            Enumerable.Range(7, 5).Dump(nameof(Enumerable.Range));

            Program.BlankLine();

            Enumerable.Repeat("Hello!", 5).Dump(nameof(Enumerable.Repeat));
        }

        private static void Run4()
        {
            // Enumerable.Range(7, 5)
            Generate(
                7,
                state => state < 7 + 5,
                state => (state + 1, state))
                .Dump("Range via Generate");

            // Enumerable.Repeat(42, 5)
            Generate(
                5,
                state => state > 0,
                state => (state - 1, "Hello!"))
                .Dump("Repeat via Generate");
        }

        private static IEnumerable<T> Generate<T, TState>(
            TState seed, Func<TState, bool> condition, Func<TState, (TState, T)> next)
        {
            var state = seed;
            while (condition(state))
            {
                T item;
                (state, item) = next(state);

                yield return item;
            }
        }
        #endregion

        #region Cata (Свёртка)
        private static void Run5()
        {
            var data = Enumerable.Range(9, 3);

            data.Sum().Dump(nameof(Enumerable.Sum));
            data.Min().Dump(nameof(Enumerable.Min));
            data.Max().Dump(nameof(Enumerable.Max));
            data.Average().Dump(nameof(Enumerable.Average));
            data.Count().Dump(nameof(Enumerable.Count));
        }

        private static void Run6()
        {
            var empty = Enumerable.Empty<string>();
            var oneItem = Return("Hello");
            var manyItems = new[] { "Hello", "World" };

            Try(() => empty.FirstOrDefault(), "empty.FirstOrDefault()");
            Try(() => oneItem.FirstOrDefault(), "oneItem.FirstOrDefault()");
            Try(() => manyItems.FirstOrDefault(), "manyItems.FirstOrDefault()");

            Program.BlankLine();

            Try(() => empty.First(), "empty.First()");
            Try(() => oneItem.First(), "oneItem.First()");
            Try(() => manyItems.First(), "manyItems.First()");

            Program.BlankLine();

            Try(() => empty.LastOrDefault(), "empty.LastOrDefault()");
            Try(() => oneItem.LastOrDefault(), "oneItem.LastOrDefault()");
            Try(() => manyItems.LastOrDefault(), "manyItems.LastOrDefault()");

            Program.BlankLine();

            Try(() => empty.Last(), "empty.Last()");
            Try(() => oneItem.Last(), "oneItem.Last()");
            Try(() => manyItems.Last(), "manyItems.Last()");

            Program.BlankLine();

            Try(() => empty.SingleOrDefault(), "empty.SingleOrDefault()");
            Try(() => oneItem.SingleOrDefault(), "oneItem.SingleOrDefault()");
            Try(() => manyItems.SingleOrDefault(), "manyItems.SingleOrDefault()");

            Program.BlankLine();

            Try(() => empty.Single(), "empty.Single()");
            Try(() => oneItem.Single(), "oneItem.Single()");
            Try(() => manyItems.Single(), "manyItems.Single()");

            Program.BlankLine();

            Try(() => empty.ElementAtOrDefault(1), "empty.ElementAtOrDefault(1)");
            Try(() => oneItem.ElementAtOrDefault(1), "oneItem.ElementAtOrDefault(1)");
            Try(() => manyItems.ElementAtOrDefault(1), "manyItems.ElementAtOrDefault(1)");

            Program.BlankLine();

            Try(() => empty.ElementAt(1), "empty.ElementAt(1)");
            Try(() => oneItem.ElementAt(1), "oneItem.ElementAt(1)");
            Try(() => manyItems.ElementAt(1), "manyItems.ElementAt(1)");


            void Try<T>(Func<T> func, string name)
            {
                try
                {
                    func().Dump(name);
                }
                catch (Exception e)
                {
                    e.Dump(name);
                }
            }
        }

        private static void Run7()
        {
            Enumerable.Empty<int>().Any().Dump("empty.Any()");
            Return(42).Any().Dump("nonempty.Any()");

            Program.BlankLine();


            var empty = Enumerable.Empty<int>();
            var negatives = new[] { -2, -1 };
            var positives = new[] { 1, 2 };
            var mixed = new[] { -2, -1, 1, 2 };

            bool IsPositive(int x) => x > 0;

            empty.Any(IsPositive).Dump("empty.Any(IsPositive)");
            empty.All(IsPositive).Dump("empty.All(IsPositive)");
            empty.Contains(2).Dump("empty.Contains(2)");

            Program.BlankLine();

            negatives.Any(IsPositive).Dump("negatives.Any(IsPositive)");
            negatives.All(IsPositive).Dump("negatives.All(IsPositive)");
            negatives.Contains(2).Dump("negatives.Contains(2)");

            Program.BlankLine();

            positives.Any(IsPositive).Dump("positives.Any(IsPositive)");
            positives.All(IsPositive).Dump("positives.All(IsPositive)");
            positives.Contains(2).Dump("positives.Contains(2)");

            Program.BlankLine();

            mixed.Any(IsPositive).Dump("mixed.Any(IsPositive)");
            mixed.All(IsPositive).Dump("mixed.All(IsPositive)");
            mixed.Contains(2).Dump("mixed.Contains(2)");
        }

        private static void Run8() // aka Fold, FoldLeft, Reduce
        {
            var data = Enumerable.Range(9, 3);

            #region Простая перегрузка Aggregate
            data.Aggregate((x, y) => x + y).Dump($"Sum of {data.ConvertToString()} via {nameof(Enumerable.Aggregate)}");
            data.Aggregate((x, y) => x * y).Dump($"Product of {data.ConvertToString()} via {nameof(Enumerable.Aggregate)}");

            Try(() => Enumerable.Empty<int>().Aggregate((x, y) => x + y), "empty.Aggregate((x, y) => x + y)");

            void Try<T>(Func<T> func, string name)
            {
                try
                {
                    func().Dump(name);
                }
                catch (Exception e)
                {
                    e.Dump(name);
                }
            }
            #endregion

            Program.BlankLine();

            #region Навороченный вариант
            data.Aggregate(0, (x, y) => x + y).Dump($"Sum of {data.ConvertToString()} via {nameof(Enumerable.Aggregate)}");
            data.Aggregate(1, (x, y) => x * y).Dump($"Product of {data.ConvertToString()} via {nameof(Enumerable.Aggregate)}");

            Enumerable.Empty<int>().Aggregate(0, (x, y) => x + y).Dump("empty.Aggregate((x, y) => x + y)");
            #endregion
        }

        private static void Run9()
        {
            var numbers = new[] { -1, 0, 1, 2, 3 };
            bool IsPositive(int x) => x > 0;

            numbers
                .Aggregate(
                    (int?)null,
                    (acc, item) => acc.GetValueOrDefault(item),
                    acc => acc.Value)
                .Dump("First via Aggregate");

            numbers
                .Aggregate(
                    (int?)null,
                    (acc, item) => item,
                    acc => acc.Value)
                .Dump("Last via Aggregate");

            numbers
                .Aggregate(
                    0,
                    (acc, item) => acc + 1,
                    acc => acc)
                .Dump("Count via Aggregate");

            numbers
                .Aggregate(
                    false,
                    (acc, item) => acc || IsPositive(item),
                    acc => acc)
                .Dump("Any via Aggregate");

            numbers
                .Aggregate(
                    true,
                    (acc, item) => acc && IsPositive(item),
                    acc => acc)
                .Dump("All via Aggregate");
        }
        #endregion

        #region Множества
        private static void Run10()
        {
            var set1 = new[] { 1, 1, 2, 2, 3, 3, 4, 4 };
            var set2 = new[] { 3, 3, 4, 4, 5, 5, 6, 6 };

            set1.Union(set2).Dump("set1.Union(set2)");

            Program.BlankLine();

            set1.Intersect(set2).Dump("set1.Intersect(set2)");

            Program.BlankLine();

            set1.Except(set2).Dump("set1.Except(set2)");
        }
        #endregion

        #region Расширение
        private static void Run11()
        {
            var first = new[] { 1, 2 };
            var second = new[] { 3, 4 };

            first.Prepend(0).Dump($"{first.ConvertToString()}.Prepend(0)");

            Program.BlankLine();

            first.Append(3).Dump($"{first.ConvertToString()}.Append(3)");

            Program.BlankLine();

            first.Concat(second).Dump($"{first.ConvertToString()}.Concat({second.ConvertToString()})");

            Program.BlankLine();

            first.DefaultIfEmpty(0).Dump($"{first.ConvertToString()}.DefaultIfEmpty(0)");
            Enumerable.Empty<int>().DefaultIfEmpty(0).Dump($"Empty.DefaultIfEmpty(0)");
        }
        #endregion

        #region Материализация
        private static void Run12()
        {
            var numbers = new[] { -1, 0, 1, 2, 2 };

            numbers.ToList().Dump(nameof(Enumerable.ToList));
            
            Program.BlankLine();

            numbers.ToArray().Dump(nameof(Enumerable.ToArray));

            Program.BlankLine();

            numbers.ToHashSet().Dump(nameof(Enumerable.ToHashSet));
        }

        private static void Run13()
        {
            var numbers = new[] { -1, 0, 1, 2, 2 };
            var uniqueNumbers = new[] { -1, 0, 1, 2 };

            uniqueNumbers.ToDictionary(x => x).Dump("uniqueNumbers.ToDictionary(x => x)");

            Program.BlankLine();

            uniqueNumbers.ToDictionary(x => x, x => x * x).Dump("uniqueNumbers.ToDictionary(x => x, x => x * x)");

            Program.BlankLine();

            numbers.ToLookup(x => x).Dump("numbers.ToLookup(x => x)");

            Program.BlankLine();

            numbers.ToLookup(x => x, x => x * x).Dump("numbers.ToLookup(x => x, x => x * x)");
        }
        #endregion

        #region Фильтрация/ усечение
        private static void Run14() // aka Filter
        {
            new[] { -1, 0, 1, 2 }.Where(x => x > 0).Dump("{ -1, 0, 1, 2 }.Where(x => x > 0)");

            Program.BlankLine();

            new[] { 1, 2, 1, 2 }.Distinct().Dump("{ 1, 2, 1, 2 }.Distinct()");

            Program.BlankLine();

            new object[] { 0, "foo", DateTime.Now, "bar" }.OfType<string>().Dump("{ 0, \"foo\", DateTime.Now, \"bar\" }.OfType<string>()");

            Program.BlankLine();
        }

        private static void Run15()
        {
            Enumerable.Range(1, 100).Skip(10).Take(5).Dump("Range(1, 100).Skip(10).Take(5)");
        }

        private static void Run16()
        {
            var data = new[] { -5, -4, 3, 2, -1, 0 };

            data.SkipWhile(x => x < 0).TakeWhile(x => x > 0).Dump($"{data.ConvertToString()}.SkipWhile(x => x < 0).TakeWhile(x => x > 0)");
        }
        #endregion

        #region Преобразование
        private static void Run17() // aka Map
        {
            new object[] { "foo", "bar" }.Cast<string>().Dump(nameof(Enumerable.Cast));

//            new int[] { 1, 2 }.Cast<double>().Dump();

            Program.BlankLine();

            var today = DateTime.Today;
            new[] { 1, 2, 3 }.Select(x => today.AddDays(x)).Dump(nameof(Enumerable.Select));
        }
        #endregion

        #region Сортировка / Порядок
        private static void Run18()
        {
            var data = new[] { 2, 1, 3 };

            data.Reverse().Dump($"{data.ConvertToString()}.Reverse()");

            Program.BlankLine();

            data.OrderBy(x => x).Dump($"{data.ConvertToString()}.OrderBy(x => x)");

            Program.BlankLine();

            data.OrderByDescending(x => x).Dump($"{data.ConvertToString()}.OrderByDescending(x => x)");
        }

        private static void Run19()
        {
            var data = new[]
            {
                new Person { Name = "Андрей", Age = 20 }, 
                new Person { Name = "Андрей", Age = 30 }, 
                new Person { Name = "Николай", Age = 25 }, 
                new Person { Name = "Николай", Age = 35 }, 
            };

            data.OrderBy(p => p.Name).ThenBy(p => p.Age).Dump("Name asc, Age asc");

            Program.BlankLine();

            data.OrderBy(p => p.Name).ThenByDescending(p => p.Age).Dump("Name asc, Age desc");

            Program.BlankLine();

            data.OrderByDescending(p => p.Name).ThenBy(p => p.Age).Dump("Name desc, Age asc");

            Program.BlankLine();

            data.OrderByDescending(p => p.Name).ThenByDescending(p => p.Age).Dump("Name desc, Age desc");
        }

        public sealed class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        #endregion

        #region Группировка
        private static void Run20()
        {
            var data = new[] { -2, -1, 4, 5, 6 };

            data.GroupBy(x => x > 0)
                .Dump($"{data.ConvertToString()}.GroupBy(x => x > 0)");

            Program.BlankLine();

            data.GroupBy(x => x > 0, x => Math.Abs(x))
                .Dump($"{data.ConvertToString()}.GroupBy(x => x > 0, x => Math.Abs(x))");

            Program.BlankLine();

            data.GroupBy(x => x > 0, (_, groupItems) => groupItems.Sum())
                .Dump($"{data.ConvertToString()}.GroupBy(x => x > 0, (_, groupItems) => groupItems.Sum())");
        }

        private static void Run21()
        {
            var persons = new[]
            {
                new { FirstName = "Иван", LastName = "Иванов" },
                new { FirstName = "Алексей", LastName = "Петров" },
                new { FirstName = "Иван", LastName = "Михайлов" },
                new { FirstName = "Иван", LastName = "Иванов" },
            };

            persons
                .GroupBy(p => p.FirstName, p => p.LastName)
                .Dump("Тёзки по имени");

            Program.BlankLine();

            persons
                .GroupBy(p => new { p.FirstName, p.LastName }, (key, group) => new { key.FirstName, key.LastName, Count = group.Count() })
                .Dump("Полные тёзки");
        }
        #endregion

        #region Комбинирование нескольких последовательностей
        private static void Run22()
        {
            var first = new[] { 1, 2, 3, 4, 5 };
            var second = new[] { "Один", "Два", "Три", "Четыре" };

            first.Zip(second, (x, y) => new { Left = x, Right = y }).Dump();

            Program.BlankLine();

            var today = DateTime.Now;
            var events = new[] { today, today.AddHours(1), today.AddHours(3), today.AddHours(6), today.AddHours(10) };

            events
                .Zip(
                    events.Skip(1),
                    (from, to) => (to - from).TotalHours)
                .Dump("Intervals");
        }

        private static void Run23()
        {
            new[] { 1, 2, 3 }.SequenceEqual(new[] { 1, 2, 3 }).Dump("{ 1, 2, 3 }.SequenceEqual({ 1, 2, 3 })");
            new[] { 1, 2, 3 }.SequenceEqual(new[] { 2, 1, 3 }).Dump("{ 1, 2, 3 }.SequenceEqual({ 2, 1, 3 })");
            new[] { 1, 2, 3 }.SequenceEqual(new[] { 1, 2, 3, 4 }).Dump("{ 1, 2, 3 }.SequenceEqual({ 1, 2, 3, 4 })");
        }

        private static void Run24()
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

            peoples.Join(cars,
                    owner => owner.Id,
                    car => car.OwnerId,
                    (owner, car) => new
                    {
                        Name = $"{owner.FirstName} {owner.LastName}",
                        car.Make
                    })
                .Dump();

            Program.BlankLine();

            peoples.Join(cars,
                    owner => new { owner.FirstName, owner.LastName },
                    car => new { FirstName = car.OwnerFirstName, LastName = car.OwnerLastName },
                    (owner, car) => new
                    {
                        Name = $"{owner.FirstName} {owner.LastName}",
                        car.Make
                    })
                .Dump();
        }

        private static void Run25()
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

            peoples.GroupJoin(cars,
                    owner => owner.Id,
                    car => car.OwnerId,
                    (owner, ownerCars) => new
                    {
                        Name = $"{owner.FirstName} {owner.LastName}",
                        Cars = ownerCars.Select(car => car.Make)
                    })
                .Dump();
        }

        private static void Run26() // aka FlatMap, Bind
        {
            var peoples = new[]
            {
                new { Name = "Андрей", Cars = new[] { "Toyota" } },
                new { Name = "Иван",   Cars = Array.Empty<string>() },
                new { Name = "Петр",   Cars = new[] { "Nissan", "Honda" } }
            };

            peoples.SelectMany(owner => owner.Cars)
                .Dump("Cars");

            Program.BlankLine();

            peoples.SelectMany(owner => owner.Cars, (owner, car) => new { Owner = owner.Name, Car = car })
                .Dump("Cars with owners");
        }

        private static void Run27()
        {
            var data = new[] { 1, 2, 3, 4, 5, 6 };

            // Select = SelectMany + Return
            data.Select(x => x * x)
                .DumpAsString("x * x via Select");

            data.SelectMany(x => Return(x * x))
                .DumpAsString("x * x via SelectMany + Return");

            Program.BlankLine();

            // Where = SelectMany + (Return | Empty)
            data.Where(x => x % 2 == 0)
                .DumpAsString("Evens via Where");

            data.SelectMany(
                    x => x % 2 == 0
                        ? Return(x)
                        : Enumerable.Empty<int>())
                .DumpAsString("Evens via SelectMany + (Return | Empty)");
        }
        #endregion
    }
}