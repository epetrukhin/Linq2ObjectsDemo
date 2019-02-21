using System;
using System.Threading.Tasks;
using ConsoleApp.Helpers;

namespace ConsoleApp.Samples
{
    internal static class Sample13_TaskMonad
    {
        // Монады в .NET — Task, Nullable<T>, Lazy, List<T>, Array и т.д.

        private static Task<T> Return<T>(T value) =>
            Task.FromResult(value);

        private static Task<TOut> Select<TIn, TOut>(
            this Task<TIn> source,
            Func<TIn, TOut> map)
        {
            return source.SelectMany(x => Return(map(x)));

//            return source.ContinueWith(t => map(t.Result));
        }

        private static Task<TOut> SelectMany<TIn, TOut>(
            this Task<TIn> source,
            Func<TIn, Task<TOut>> resultSelector)
        {
            return source.SelectMany(resultSelector, (_, result) => result);

//            return source.ContinueWith(t => resultSelector(t.Result)).Unwrap();
        }

        private static Task<TOut> SelectMany<TIn, TEx, TOut>(
            this Task<TIn> source,
            Func<TIn, Task<TEx>> selector,
            Func<TIn, TEx, TOut> resultSelector)
        {
            return source
                .ContinueWith(t => selector(t.Result)).Unwrap()
                .ContinueWith(t => resultSelector(source.Result, t.Result));
        }

        public static void Run()
        {
            async Task<double> LoadFromDb()
            {
                "Begin load from db".WriteInfo();
                await Task.Delay(1000);
                "End load from db".WriteInfo();

                return 40.3;
            }

            async Task<int> Square(int x)
            {
                "Begin square calc".WriteInfo();
                await Task.Delay(1000);
                "End square calc".WriteInfo();

                return x * x;
            }

            async Task<int> Negate(int x)
            {
                "Begin negate".WriteInfo();
                await Task.Delay(1000);
                "End negate".WriteInfo();

                return -x;
            }

            Task<int> resultTask =
                from dbValue in LoadFromDb()
                let roundedValue = (int)Math.Round(dbValue)
                from square in Square(roundedValue)
                from negated in Negate(square)
                select negated;

            resultTask.Result.Dump("Result");
        }
    }
}