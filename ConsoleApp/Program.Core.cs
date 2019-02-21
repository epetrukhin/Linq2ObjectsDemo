using System;
using System.Text;
using System.Threading;
using ConsoleApp.Helpers;

namespace ConsoleApp
{
    internal static partial class Program
    {
        [STAThread]
        private static void Main()
        {
            Console.WindowWidth = Console.LargestWindowWidth - 70;
            Console.OutputEncoding = Encoding.UTF8;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            try
            {
                ProgramCode();
            }
            catch (Exception ex)
            {
                DumpException(ex);
            }
            finally
            {
                AppDomain.CurrentDomain.UnhandledException -= CurrentDomainOnUnhandledException;
            }

            WaitKey("Press any key to close...");
        }

        private static void CurrentDomainOnUnhandledException(object _, UnhandledExceptionEventArgs args) =>
            DumpException((Exception)args.ExceptionObject);

        public static void DumpException(Exception ex)
        {
            Console.WriteLine();
            using (ForegroundColor(ConsoleHelpers.ErrorColor))
            {
                SeparatorLine();
                ex.ToString().WriteLine();
                SeparatorLine();
            }
            Console.WriteLine();
        }

        public static void BlankLine() => Console.WriteLine();

        public static void ClearConsole() => Console.Clear();

        public static void SeparatorLine() => Console.Write(new string('=', Console.WindowWidth));

        private static void WaitKey(string prompt = null)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                prompt.WriteWarning();

            Console.ReadKey();
        }

        private static void Sleep(int milliseconds) => Thread.Sleep(milliseconds);

        private static void Sleep(TimeSpan interval) => Thread.Sleep(interval);

        public static IDisposable ForegroundColor(ConsoleColor color) => ConsoleHelpers.WithForegroundColor(color);
    }
}