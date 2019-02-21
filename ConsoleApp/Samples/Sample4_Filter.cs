using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample4_Filter
    {
        private sealed class FilterEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _source;
            private readonly Func<T, bool> _predicate;

            public FilterEnumerable(IEnumerable<T> source, Func<T, bool> predicate)
            {
                _source = source ?? throw new ArgumentNullException(nameof(source));
                _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<T> GetEnumerator() => new FilterEnumerator<T>(_source.GetEnumerator(), _predicate);
        }

        private sealed class FilterEnumerator<T> : IEnumerator<T>
        {
            private readonly IEnumerator<T> _source;
            private readonly Func<T, bool> _predicate;

            public FilterEnumerator(IEnumerator<T> source, Func<T, bool> predicate)
            {
                _source = source;
                _predicate = predicate;
            }

            public bool MoveNext()
            {
                while (_source.MoveNext())
                {
                    Current = _source.Current;

                    if (_predicate(Current))
                        return true;
                }

                return false;
            }

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _source.Dispose();
            }

            public void Reset() => throw new NotSupportedException();
        }

        public static void Run()
        {
            bool IsEven(int x) => x % 2 == 0;
            var source = new[] { 1, 2, 3, 4, 5, 6, 7 };

            var filterEnumerable = new FilterEnumerable<int>(source, IsEven);
            filterEnumerable.Dump();
        }
    }
}