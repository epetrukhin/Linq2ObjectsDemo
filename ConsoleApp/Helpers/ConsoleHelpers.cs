using System;

namespace ConsoleApp.Helpers
{
    internal static class ConsoleHelpers
    {
        public const ConsoleColor ErrorColor   = ConsoleColor.Red;
        public const ConsoleColor WarningColor = ConsoleColor.Yellow;
        public const ConsoleColor InfoColor    = ConsoleColor.DarkGreen;

        public static void WriteLine(this string text) => Console.WriteLine(text ?? "<null>");

        public static void WriteError(this string text)   => text.WriteWithColor(ErrorColor);
        public static void WriteWarning(this string text) => text.WriteWithColor(WarningColor);
        public static void WriteInfo(this string text)    => text.WriteWithColor(InfoColor);

        public static void WriteWithColor(this string text, ConsoleColor color)
        {
            using (WithForegroundColor(color))
                text.WriteLine();
        }

        public static IDisposable WithForegroundColor(ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;

            if (currentColor == color)
                return new Disposable(null);

            Console.ForegroundColor = color;

            return new Disposable(() => Console.ForegroundColor = currentColor);
        }

        private sealed class Disposable : IDisposable
        {
            private Action _dispose;

            public Disposable(Action dispose) => _dispose = dispose;

            public void Dispose()
            {
                var temp = _dispose;
                _dispose = null;

                temp?.Invoke();
            }
        }
    }
}