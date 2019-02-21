using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Helpers;

namespace ConsoleApp.Samples
{
    internal static class Sample10_LazyEvaluation
    {
        public static void Run()
        {
            var numbers = new List<int> { 1, 2, 3, 4 };

            var evenNumbers = numbers.Where(x => x % 2 == 0); // [ 2, 4 ]

            numbers.Add(6);

            evenNumbers.DumpAsString("After Add(6)");

            numbers.Add(10);

            evenNumbers.DumpAsString("After Add(10)");
        }
    }
}