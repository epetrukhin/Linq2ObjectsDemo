using System;
using System.Collections.Generic;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample7_ExtensionMethods
    {
        public static void Run()
        {
            var data = new[] { -3, -2, -1, 0, 1, 2, 3 };

            // Сумма квадратов первых двух положительных чисел (1 * 1 + 2 * 2 = 5)
            var sum = Sum(Map(Top(Filter(data, x => x > 0), 2), x => x * x));
            //         4   3   2    1
            sum.Dump();

            var sum2 =
                Sum(
                    Map(
                        Top(
                            Filter(
                                data,
                                x => x > 0),
                            2),
                        x => x * x));

            sum2.Dump();

            #region Via extension methods
            var sum3 = data.Filter(x => x > 0).Top(2).Map(x => x * x).Sum();

            sum3.Dump();


            var sum4 = data
                .Filter(x => x > 0)
                .Top(2)
                .Map(x => x * x)
                .Sum();

            sum4.Dump();
            #endregion
        }

        private static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }

        private static IEnumerable<T> Top<T>(this IEnumerable<T> source, int count)
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

        private static IEnumerable<TOut> Map<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> map)
        {
            foreach (var item in source)
            {
                yield return map(item);
            }
        }

        private static int Sum(this IEnumerable<int> source)
        {
            var sum = 0;

            foreach (var item in source)
            {
                sum += item;
            }

            return sum;
        }
    }
}