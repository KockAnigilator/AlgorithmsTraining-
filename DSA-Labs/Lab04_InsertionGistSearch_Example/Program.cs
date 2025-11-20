using Lab04_InsertionGistSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04_InsertionGistSearch_Example
{
    using System;

    namespace Lab04_InsertionGistSearch
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
                GistInsertionSorter.Sort(array);
                Console.WriteLine(string.Join(", ", array));

                Console.WriteLine("\nРезультаты анализа:");
                Console.WriteLine($"Сравнений:               {stats.Comparisons}");
                Console.WriteLine($"Сдвигов:                 {stats.Shifts}");
                Console.WriteLine($"Операций (всего):        {stats.Comparisons + stats.Shifts}");
                Console.WriteLine($"Практическая сложность:  ~{k:F4} · n²");
                Console.WriteLine($"Время выполнения:        {time.TotalMilliseconds:F4} мс");

                Console.ReadKey();
            }
        }
    }

}
