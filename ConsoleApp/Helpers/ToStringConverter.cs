using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp.Helpers
{
    internal static class ToStringConverter
    {
        #region Tuples
        private static string Convert<T1, T2, T3, T4, T5, T6, T7, TRest>(Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(", ")
                .Append(tuple.Item6.ConvertToString())
                .Append(", ")
                .Append(tuple.Item7.ConvertToString())
                .Append(", ")
                .Append(tuple.Rest.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4, T5, T6, T7>(Tuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(", ")
                .Append(tuple.Item6.ConvertToString())
                .Append(", ")
                .Append(tuple.Item7.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4, T5, T6>(Tuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())             
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(", ")
                .Append(tuple.Item6.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4, T5>(Tuple<T1, T2, T3, T4, T5> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3>(Tuple<T1, T2, T3> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2>(Tuple<T1, T2> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1>(Tuple<T1> tuple)
        {
            Debug.Assert(tuple != null);

            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(")")
                .ToString();
        }
        #endregion

        #region Value Tuples
        private static string Convert<T1, T2, T3, T4, T5, T6, T7, TRest>(ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple)
            where TRest : struct
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(", ")
                .Append(tuple.Item6.ConvertToString())
                .Append(", ")
                .Append(tuple.Item7.ConvertToString())
                .Append(", ")
                .Append(tuple.Rest.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4, T5, T6, T7>(ValueTuple<T1, T2, T3, T4, T5, T6, T7> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(", ")
                .Append(tuple.Item6.ConvertToString())
                .Append(", ")
                .Append(tuple.Item7.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4, T5, T6>(ValueTuple<T1, T2, T3, T4, T5, T6> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(", ")
                .Append(tuple.Item6.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4, T5>(ValueTuple<T1, T2, T3, T4, T5> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(", ")
                .Append(tuple.Item5.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3, T4>(ValueTuple<T1, T2, T3, T4> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(", ")
                .Append(tuple.Item4.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2, T3>(ValueTuple<T1, T2, T3> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(", ")
                .Append(tuple.Item3.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1, T2>(ValueTuple<T1, T2> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(", ")
                .Append(tuple.Item2.ConvertToString())
                .Append(")")
                .ToString();
        }

        private static string Convert<T1>(ValueTuple<T1> tuple)
        {
            return new StringBuilder("(")
                .Append(tuple.Item1.ConvertToString())
                .Append(")")
                .ToString();
        }
        #endregion

        #region Concrete types to string
        private static string Convert<T>(IEnumerable<T> sequence) =>
            "[" + string.Join(", ", sequence.Select(item => item.ConvertToString())) + "]";

        private static string Convert<TKey, TElement>(IGrouping<TKey, TElement> group) =>
            group.Key.ConvertToString() + " => " + group.ToList().ConvertToString();

        private static string Convert<TKey, TValue>(KeyValuePair<TKey, TValue> kvp) =>
            kvp.Key.ConvertToString() + " => " + kvp.Value.ConvertToString();

        private static string Convert(string str) => $"\"{str}\"";

        private static string Convert(char c) => $"\'{c}\'";

        private static string Convert(Exception ex) =>
            $"{ex.GetType().Name}({ex.Message})";

        private static string Convert(object obj) =>
            obj.ToString();
        #endregion

        public static string ConvertToString(this object obj) =>
            obj == null
                ? "<null>"
                : (string)Convert((dynamic)obj);
    }
}