using System;
using System.Collections.Generic;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample8_AnonymousTypes
    {
        private enum Gender
        {
            Man,
            Woman
        }

        private sealed class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public DateTime Birthdate { get; set; }
        }

        private static readonly Person[] Persons =
        {
            new Person
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Gender = Gender.Man,
                Birthdate = new DateTime(1990, 04, 15)
            },
            new Person
            {
                FirstName = "Анна",
                LastName = "Петрова",
                Gender = Gender.Woman,
                Birthdate = new DateTime(1995, 10, 06)
            }
        };

        public static void Run()
        {
//            Run1();
//            Run2();
//            Run3();
//            Run4();
            Run5();
        }

        private static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }

        private static IEnumerable<TOut> Map<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> map)
        {
            foreach (var item in source)
            {
                yield return map(item);
            }
        }

        private static void PrintPersonInfo(string fullName, bool isMen, int age)
        {
            $"{fullName} ({(isMen ? "м" : "ж")}., полных лет: {age})".WriteLine();
        }

        #region Nominal types
        private sealed class PersonEx
        {
            public string FullName { get; set; }
            public bool IsMen { get; set; }
            public int Age { get; set; }
        }

        private static void Run1()
        {
            var today = DateTime.Today;

            var personsToPrint = Persons
                .Map(p => new PersonEx
                {
                    FullName = $"{p.FirstName} {p.LastName}",
                    IsMen = p.Gender == Gender.Man,
                    Age = (today - p.Birthdate).Days / 365
                })
                .Filter(p => p.Age > 18);

            foreach (var person in personsToPrint)
            {
                PrintPersonInfo(person.FullName, person.IsMen, person.Age);
            }
        }
        #endregion

        #region Anonymous types
        private static void Run2()
        {
            var today = DateTime.Today;

            var personsToPrint = Persons
                .Map(p => new
                {
                    FullName = $"{p.FirstName} {p.LastName}",
                    IsMen = p.Gender == Gender.Man,
                    Age = (today - p.Birthdate).Days / 365
                })
                .Filter(p => p.Age > 18);

            foreach (var person in personsToPrint)
            {
                PrintPersonInfo(person.FullName, person.IsMen, person.Age);
            }
        }
        #endregion

        #region Tuples
        private static void Run3()
        {
            var today = DateTime.Today;

            var personsToPrint = Persons
                .Map(p => Tuple.Create
                (
                    $"{p.FirstName} {p.LastName}",
                    p.Gender == Gender.Man,
                    (today - p.Birthdate).Days / 365
                ))
                .Filter(p => p.Item3 > 18);

            foreach (var person in personsToPrint)
            {
                PrintPersonInfo(person.Item1, person.Item2, person.Item3);
            }
        }
        #endregion

        #region Value Tuples
        private static void Run4()
        {
            var today = DateTime.Today;

            var personsToPrint = Persons
                .Map(p =>
                (
                    FullName: $"{p.FirstName} {p.LastName}",
                    IsMen: p.Gender == Gender.Man,
                    Age: (today - p.Birthdate).Days / 365
                ))
                .Filter(p => p.Age > 18);

            foreach (var person in personsToPrint)
            {
                PrintPersonInfo(person.FullName, person.IsMen, person.Age);
            }
        }
        #endregion

        #region Anonymous types internals
        private static void Run5()
        {
            var first = new { Name = "Андрей", Age = 20 };
            var firstCopy = new { Name = "Андрей", Age = 20 };
            var second = new { Name = "Иван", Age = 20 };

            first.ToString().Dump("First");
            second.ToString().Dump("Second");

            Program.BlankLine();

            ReferenceEquals(first, firstCopy).Dump("ReferenceEquals(first, firstCopy)");
            ReferenceEquals(first, second).Dump("ReferenceEquals(first, second)");
            ReferenceEquals(firstCopy, second).Dump("ReferenceEquals(firstCopy, second)");
            
            Program.BlankLine();

            (first == firstCopy).Dump("first == firstCopy");
            (first == second).Dump("first == second");

            Program.BlankLine();

            first.Equals(firstCopy).Dump("first.Equals(firstCopy)");
            first.Equals(second).Dump("first.Equals(second)");
        }
        #endregion
    }
}