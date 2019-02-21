using System.Collections.Generic;

namespace ConsoleApp.Samples
{
    internal static class Sample6_Covariance
    {
        private abstract class Animal
        {}

        private sealed class Fox : Animal
        {}

        public static void Run()
        {
            Fox fox = new Fox();
            Animal animal = fox;

            List<Fox> foxesList = new List<Fox>();
//            List<Animal> animalsList = foxesList; // Не компилируется!

            Fox[] foxesArray = new Fox[0];
            Animal[] animalsArray = foxesArray;

            IEnumerable<Fox> foxesEnumerable = new List<Fox>();
            IEnumerable<Animal> animalsEnumerable = foxesEnumerable;

            // https://blogs.msdn.microsoft.com/csharpfaq/2010/02/16/covariance-and-contravariance-faq/
        }
    }
}