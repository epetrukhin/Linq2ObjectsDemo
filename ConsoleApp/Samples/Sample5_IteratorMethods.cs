using System;
using System.Collections.Generic;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample5_IteratorMethods
    {
        public static void Run()
        {
//            Run1();
//            Run2();
            Run3();
        }

        #region 1
        private static void Run1()
        {
            var counter = 0;

            foreach (var rnd in Randoms())
            {
                rnd.Dump();

                counter++;

                if (counter >= 5)
                    break;
            }
        }

        private static IEnumerable<int> Randoms()
        {
            var rnd = new Random();

            while (true)
            {
                yield return rnd.Next();
            }
        }
        #endregion

        #region 2
        private static void Run2()
        {
            bool IsEven(int x) => x % 2 == 0;
            var source = new[] { 1, 2, 3, 4, 5, 6, 7 };

            var filterEnumerable = Filter(source, IsEven);
            filterEnumerable.Dump();
        }

        private static IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
        #endregion

        #region 3
        private static void Run3()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Top(source, 3).Dump();

            #region Exception
//            try
//            {
//                var top = Top<int>(null, 3);
//
//                "Начинаем перебор...".WriteInfo();
//
//                top.Dump();
//            }
//            catch (Exception e)
//            {
//                Program.DumpException(e);
//            }
            #endregion
        }

        private static IEnumerable<T> Top<T>(IEnumerable<T> source, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return Enumerable();

            IEnumerable<T> Enumerable()
            {
                if (count <= 0)
                    yield break;

                foreach (var item in source)
                {
                    yield return item;
                    if (--count == 0)
                        break;
                }
            }
        }
        #endregion
    }
}