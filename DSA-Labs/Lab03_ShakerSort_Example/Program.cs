using Lab03_ShakerSort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_ShakerSort_Example
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите массив через пробел:");
            int[] array = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine("\nИсходный массив:");
            Console.WriteLine(string.Join(", ", array));

            SortingAnalysis.PrintTheory();

            var (time, stats, k) = SortingAnalysis.Measure(array);

            Console.WriteLine("\nОтсортированный массив:");
            ShakerSorter.Sort(array);
            Console.WriteLine(string.Join(", ", array));

            Console.WriteLine("\nРезультаты анализа:");
            Console.WriteLine($"Сравнений:               {stats.Comparisons}");
            Console.WriteLine($"Обменов:                 {stats.Swaps}");
            Console.WriteLine($"Операций (всего):        {stats.Comparisons + stats.Swaps}");
            Console.WriteLine($"Практическая сложность:  ~{k:F4} · n²");
            Console.WriteLine($"Время выполнения:        {time.TotalMilliseconds:F4} мс");

            Console.ReadKey();
        }
    }
}
