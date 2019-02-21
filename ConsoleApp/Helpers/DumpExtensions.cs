using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alba.CsConsoleFormat;

namespace ConsoleApp.Helpers
{
    internal static class DumpExtensions
    {
        private const int DefaultMaxItemsCount = 50;

        #region Type helpers
        private static readonly HashSet<Type> ScalarTypes = new HashSet<Type>
        {
            typeof(object),
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(TimeSpan)
        };
        private static bool IsScalar(this Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return
                type.IsPrimitive ||
                type.IsEnum ||
                ScalarTypes.Contains(type) ||
                IsGenericType(type, typeof(Nullable<>));

            bool IsGenericType(Type concreteType, Type genericType) =>
                concreteType.IsGenericType &&
                concreteType.GetGenericTypeDefinition() == genericType;
        }

        private static IEnumerable<(string name, Type type, Func<T, string> getter)> GetProperties<T>()
        {
            if (typeof(T).IsScalar())
                return new (string name, Type type, Func<T, string> getter)[]{ (typeof(T).Name, typeof(T), item => item.ConvertToString()) };

            return typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(pi => pi.GetMethod != null)
                .Select<PropertyInfo, (string name, Type type, Func<T, string> getter)>(
                    pi => (pi.Name, pi.PropertyType, item => GetPropertyValue(pi, item)));

            string GetPropertyValue(PropertyInfo pi, object item)
            {
                try
                {
                    return pi.GetValue(item).ConvertToString();
                }
                catch (Exception ex)
                {
                    return $"<{ex.Message}>";
                }
            }
        }
        #endregion

        #region Dump helpers
        private static void DumpSequence<T>(
            IEnumerable<T> sequence, int maxItems, object name, Action<IEnumerable<T>> dumpItems)
        {
            var allItems = sequence.ToList();
            var totalCount = allItems.Count;

            GetHeaderText().WriteInfo();

            dumpItems(allItems.Take(maxItems));

            if (totalCount > maxItems)
                $"... {totalCount - maxItems} tail item(s) skipped.".WriteInfo();

            string GetHeaderText()
            {
                var nameString = name?.ToString();

                var result = string.IsNullOrWhiteSpace(nameString)
                    ? string.Empty
                    : $"{nameString}, ";

                result += $"{totalCount} item(s)";

                if (totalCount > maxItems)
                    result += $", top {maxItems} item(s)";

                return result + ":";
            }
        }
        #endregion

        #region As string
        public static void DumpAsString(this object obj, object name = null)
        {
            var nameString = name?.ToString();

            if (!string.IsNullOrWhiteSpace(nameString))
            {
                using (ConsoleHelpers.WithForegroundColor(ConsoleHelpers.InfoColor))
                {
                    Console.Write($"{nameString}: ");
                }
            }

            obj.ConvertToString().WriteLine();
        }
        #endregion

        #region As list
        public static void DumpAsList<T>(this IEnumerable<T> sequence, int maxItems, object name = null)
        {
            if (maxItems <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxItems), maxItems, $"{nameof(maxItems)} must be positive");

            if (sequence == null)
            {
                DumpAsString(null, name);
                return;
            }

            DumpSequence(
                sequence,
                maxItems,
                name,
                items => string
                    .Join(
                        Environment.NewLine,
                        items.Select(item => item.ConvertToString()))
                    .WriteLine());
        }

        public static void DumpAsList<T>(this IEnumerable<T> sequence, object name = null) =>
            sequence.DumpAsList(DefaultMaxItemsCount, name);
        #endregion

        #region As table
        private static readonly HashSet<Type> RightAlignTypes = new HashSet<Type>
        {
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(short),
            typeof(ushort),
            typeof(byte),
            typeof(decimal),
            typeof(double),
            typeof(float)
        };
        private static Align GetAligmentFor(Type type) =>
            RightAlignTypes.Contains(type) ? Align.Right : Align.Left;

        private static readonly LineThickness HeaderThickness = new LineThickness(LineWidth.Single, LineWidth.Double);

        private static Document CreateDocumentCore<T>(
            IEnumerable<T> sequence,
            params (string name, Type type, Func<T, string> getter)[] props)
        {
            var grid = new Grid();

            foreach (var _ in props)
                grid.Columns.Add(new Column());

            foreach (var prop in props)
                grid.Children.Add(new Cell(prop.name) { Stroke = HeaderThickness, Align = GetAligmentFor(prop.type) });

            var cells =
                from item in sequence
                from prop in props
                select new Cell(prop.getter(item)) { Align = GetAligmentFor(prop.type) };

            foreach (var cell in cells)
                grid.Children.Add(cell);

            return new Document { Children = { grid } };
        }

        private static Document CreateDocument<TKey, TElement>(IEnumerable<IGrouping<TKey, TElement>> sequence) =>
            CreateDocumentCore(
                sequence,
                ("Key", typeof(TKey), group => group.Key.ConvertToString()),
                ("Group", typeof(IGrouping<TKey, TElement>), group => group.ToList().ConvertToString()));

        private static Document CreateDocument<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> sequence) =>
            CreateDocumentCore(
                sequence,
                ("Key", typeof(TKey), kvp => kvp.Key.ConvertToString()),
                ("Value", typeof(TValue), kvp => kvp.Value.ConvertToString()));

        private static Document CreateDocument<T>(IEnumerable<IEnumerable<T>> sequence) =>
            CreateDocumentCore(
                sequence,
                ("Item", typeof(IEnumerable<T>), items => items.ConvertToString()));

        private static Document CreateDocument<T>(IEnumerable<T[]> sequence) =>
            CreateDocumentCore(
                sequence,
                ("Item", typeof(IEnumerable<T>), items => items.ConvertToString()));

        private static Document CreateDocument<T>(IEnumerable<T> sequence) =>
            CreateDocumentCore(
                sequence,
                GetProperties<T>().ToArray());

        public static void DumpAsTable<T>(this IEnumerable<T> sequence, int maxItems, object name = null)
        {
            if (maxItems <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxItems), maxItems, $"{nameof(maxItems)} must be positive");

            if (sequence == null)
            {
                DumpAsString(null, name);
                return;
            }

            DumpSequence(
                sequence,
                maxItems,
                name,
                items => ConsoleRenderer.RenderDocument(CreateDocument((dynamic)items)));
        }

        public static void DumpAsTable<T>(this IEnumerable<T> sequence, object name = null) =>
            sequence.DumpAsTable(DefaultMaxItemsCount, name);
        #endregion

        #region Dump
        private static void DumpCore<T>(IEnumerable<T> sequence, object name)
        {
            if (typeof(T).IsScalar())
                sequence.DumpAsList(name);
            else
                sequence.DumpAsTable(name);
        }

        private static void DumpCore(string str, object name) =>
            str.DumpAsString(name);

        private static void DumpCore(object obj, object name) =>
            obj.DumpAsString(name);

        public static void Dump(this object obj, object name = null)
        {
            if (obj == null)
            {
                DumpAsString(null, name);
                return;
            }

            DumpCore((dynamic)obj, name);
        }
        #endregion
    }
}