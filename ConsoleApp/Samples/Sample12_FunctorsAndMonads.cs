using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Helpers;

namespace ConsoleApp.Samples
{
    // https://ericlippert.com/category/monads/
    // http://blog.ploeh.dk/2017/10/04/from-design-patterns-to-category-theory/
    // http://rdf.ru/files/bartozh-teorcat.pdf
    internal static class Sample12_FunctorsAndMonads
    {
        public static void Run()
        {
//            Run1();
//            Run2();
        }

        #region Functor
        // Identity
        private static T Id<T>(T x) => x;

        private static void Run1()
        {
            // У функторов есть функция Select (fmap)

            var data = new[] { 1, 2, 3 };

            #region Первый закон
            //data.Select(Id) ≡ data

            data.Select(Id)
                .DumpAsString("{ 1, 2, 3 }.Select(Id)");
            #endregion

            Program.BlankLine();

            #region Второй закон
            // data.Select(Func1).Select(Func2) ≡ data.Select(x => Func2(Func1(x)))

            int Func1(int x) => x + 1;
            int Func2(int x) => x * x;

            data.Select(Func1).Select(Func2)
                .DumpAsString("{ 1, 2, 3 }.Select(Func1).Select(Func2)");

            data.Select(x => Func2(Func1(x)))
                .DumpAsString("{ 1, 2, 3 }.Select(x => Func2(Func1(x)))");
            #endregion
        }
        #endregion

        #region Monad
        private static IEnumerable<T> Return<T>(T x)
        {
            yield return x;
        }

        private static void Run2()
        {
            // У монад есть:
            // 1. Функция Return (return)
            // 2. Функция SelectMany (>>=)

            #region Первый закон
            // Return(x).SelectMany(Func) ≡ Func(x)

            IEnumerable<string> SplitWords(string x) => x.Split(' ');

            Return("hello world").SelectMany(SplitWords)
                .DumpAsString("Return(\"hello world\").SelectMany(SplitWords)");

            SplitWords("hello world")
                .DumpAsString("SplitWords(\"hello world\")");
            #endregion

            Program.BlankLine();

            #region Второй закон
            // data.SelectMany(Return) ≡ data

            var numbers = new[] { 1, 2, 3 };

            numbers.SelectMany(Return)
                .DumpAsString("{ 1, 2, 3 }.SelectMany(Return)");

            numbers
                .DumpAsString("{ 1, 2, 3 }");
            #endregion

            Program.BlankLine();

            #region Третий закон
            // data.SelectMany(Func1).SelectMany(Func2) ≡ data.SelectMany(x => Func1(x).SelectMany(Func2))

            var names = new[] { "Анна Иванова", "John Doe" };

            IEnumerable<char> GetChars(string x) => x;

            names.SelectMany(SplitWords).SelectMany(GetChars)
                .DumpAsString("{ \"Анна Иванова\", \"John Doe\" }.SelectMany(SplitWords).SelectMany(GetChars)");

            names.SelectMany(x => SplitWords(x).SelectMany(GetChars))
                .DumpAsString("{ \"Анна Иванова\", \"John Doe\" }.SelectMany(x => SplitWords(x).SelectMany(GetChars))");
            #endregion
        }
        #endregion
    }
}