using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample3_Random
    {
        private sealed class RandomsEnumerable : IEnumerable<int>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<int> GetEnumerator() => new RandomsEnumerator();
        }

        private sealed class RandomsEnumerator : IEnumerator<int>
        {
            private readonly Random _rnd = new Random();

            public bool MoveNext() => true;

            public int Current => _rnd.Next();


            object IEnumerator.Current => Current;

            public void Dispose()
            {}

            public void Reset() => throw new NotSupportedException();
        }

        public static void Run()
        {
            var counter = 0;

            foreach (var rnd in new RandomsEnumerable())
            {
                rnd.Dump();

                counter++;

                if (counter >= 5)
                    break;
            }
        }
    }
}