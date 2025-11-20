using System;
using System.Diagnostics;

namespace Lab05_SearchSortAnalysis.Analysis
{
    public static class SearchAnalysis
    {
        public static TimeSpan MeasureTime(Func<int[], int, object> searcher, int[] arr, int value, out object stats)
        {
            Stopwatch sw = Stopwatch.StartNew();
            stats = searcher(arr, value);
            sw.Stop();

            return sw.Elapsed;
        }

        public static void PrintTheoryLinear()
        {
            Console.WriteLine("Теоретическая сложность линейного поиска: O(n)");
            Console.WriteLine("Асимптотика: n");
        }

        public static void PrintTheoryBinary()
        {
            Console.WriteLine("Теоретическая сложность бинарного поиска: O(log n)");
            Console.WriteLine("Асимптотика: log n");
        }
    }
}
