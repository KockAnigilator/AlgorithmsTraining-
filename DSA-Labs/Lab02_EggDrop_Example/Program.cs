using Lab02_EggDrop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02_EggDrop_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество этажей: ");
            int floors = int.Parse(Console.ReadLine());

            int minAttempts = EggDropSolver.MinAttempts(floors);

            Console.WriteLine();
            Console.WriteLine($"Минимальное число бросков в худшем случае: {minAttempts}");

            var seq = EggDropSolver.BuildStrategy(floors);

            Console.WriteLine("\nСтратегия бросков по этажам:");
            Console.WriteLine(string.Join(" ", seq));

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }

}
