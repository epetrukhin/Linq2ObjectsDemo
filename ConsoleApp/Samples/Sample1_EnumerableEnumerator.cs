using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConsoleApp.Helpers;
// ReSharper disable All

namespace ConsoleApp.Samples
{
    internal static class Sample1_EnumerableEnumerator
    {
        private sealed class CustomEnumerable<T> : IEnumerable</*out*/ T>/*, IEnumerable*/
        {
            private readonly T[] _data;

            public CustomEnumerable(T[] data) => _data = data;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<T> GetEnumerator() => new CustomEnumerator<T>(_data);
        }

        private sealed class CustomEnumerator<T> : IEnumerator</*out*/ T>/*, IEnumerator, IDisposable*/
        {
            private readonly T[] _data;

            public CustomEnumerator(T[] data) => _data = data;

            private static void LogMethodCall([CallerMemberName] string methodName = default)
            {
                methodName.WriteInfo();
            }

            private int _currentIndex = -1;

            public bool MoveNext()
            {
                LogMethodCall();

                _currentIndex = _currentIndex + 1;
                return _currentIndex < _data.Length;
            }

            public T Current
            {
                get
                {
                    LogMethodCall();

                    return _data[_currentIndex];
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                LogMethodCall();
            }

            public void Reset()
            {
                LogMethodCall();

                _currentIndex = -1;
            }
        }

        public static void Run()
        {
//            Run1();
//            Run2();
//            Run3();
//            Run4();
            Run5();
        }

        private static void Run1() // Аналог foreach
        {
            IEnumerable<int> customEnumerable = new CustomEnumerable<int>(new[] { 1, 2, 3 });

            "-=Вручную=-".WriteWarning();
            using (IEnumerator<int> enumerator = customEnumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    current.Dump();
                }
            }

            Program.BlankLine();

            "-=Через foreach=-".WriteWarning();
            foreach (int current in customEnumerable)
            {
                current.Dump();
            }
        }

        private static void Run2() // Reset
        {
            IEnumerable<int> customEnumerable = new CustomEnumerable<int>(new[] { 1, 2, 3 });

            using (IEnumerator<int> enumerator = customEnumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    current.Dump();
                }

                enumerator.Reset();

                Program.SeparatorLine();

                while (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    current.Dump();
                }
            }
        }

        private static void Run3() // Частичный перебор
        {
            IEnumerable<int> customEnumerable = new CustomEnumerable<int>(new[] { 1, 2, 3 });

            "-=Вручную=-".WriteWarning();
            using (IEnumerator<int> enumerator = customEnumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    current.Dump();

                    if (current == 2)
                        break;
                }
            }

            Program.BlankLine();

            "-=Через foreach=-".WriteWarning();
            foreach (int current in customEnumerable)
            {
                current.Dump();

                if (current == 2)
                    break;
            }
        }

        private static void Run4() // Перебор с пропусками
        {
            IEnumerable<int> customEnumerable = new CustomEnumerable<int>(new[] { 1, 2, 3, 4, 5 });

            using (IEnumerator<int> enumerator = customEnumerable.GetEnumerator())
            {
                while (enumerator.MoveNext() && enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    current.Dump();
                    Program.BlankLine();
                }
            }
        }

        private static void Run5() // Параллельный перебор
        {
            IEnumerable<int> customEnumerable = new CustomEnumerable<int>(new[] { 1, 2, 3 });

            using (IEnumerator<int> enumerator1 = customEnumerable.GetEnumerator())
            using (IEnumerator<int> enumerator2 = customEnumerable.GetEnumerator())
            {
                nameof(enumerator1).WriteWarning();
                enumerator1.MoveNext();
                enumerator1.Current.Dump();
                Program.BlankLine();

                nameof(enumerator1).WriteWarning();
                enumerator1.MoveNext();
                enumerator1.Current.Dump();
                Program.BlankLine();

                nameof(enumerator2).WriteWarning();
                enumerator2.MoveNext();
                enumerator2.Current.Dump();
                Program.BlankLine();

                nameof(enumerator1).WriteWarning();
                enumerator1.MoveNext();
                enumerator1.Current.Dump();
                Program.BlankLine();

                nameof(enumerator2).WriteWarning();
                enumerator2.MoveNext();
                enumerator2.Current.Dump();
            }
        }
    }
}