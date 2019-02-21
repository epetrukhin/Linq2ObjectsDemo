using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample2_ForeachDuckTyping
    {
        private sealed class CustomEnumerable<T>
        {
            private readonly T[] _data;

            public CustomEnumerable(T[] data) => _data = data;

            public CustomEnumerator<T> GetEnumerator() => new CustomEnumerator<T>(_data);
        }

        private sealed class CustomEnumerator<T>
        {
            private readonly T[] _data;

            public CustomEnumerator(T[] data) => _data = data;

            private int _currentIndex = -1;

            public bool MoveNext()
            {
                _currentIndex = _currentIndex + 1;
                return _currentIndex < _data.Length;
            }

            public T Current => _data[_currentIndex];
        }

        public static void Run()
        {
            CustomEnumerable<int> customEnumerable = new CustomEnumerable<int>(new[] { 1, 2, 3 });

            foreach (int current in customEnumerable)
            {
                current.Dump();
            }
        }
    }
}