using System;
using System.Diagnostics;

namespace Lab03_ShakerSort
{
    /// <summary>
    /// Анализ сортировки: время выполнения и вычисление практической сложности.
    /// </summary>
    public static class SortingAnalysis
    {
        public static (TimeSpan time, ShakerSorter.SortResult stats, double k) Measure(int[] arr)
        {
            int[] copy = (int[])arr.Clone();
            int n = copy.Length;

            Stopwatch sw = Stopwatch.StartNew();
            var stats = ShakerSorter.Sort(copy);
            sw.Stop();

            int ops = stats.Comparisons + stats.Swaps;
            double k = (double)ops / (n * n);

            return (sw.Elapsed, stats, k);
        }

        public static void PrintTheory()
        {
            Console.WriteLine("Теоретическая сложность:   O(n²)");
            Console.WriteLine("Асимптотическая форма:     n²");
        }
    }
}
