using System;
using System.Diagnostics;

namespace Lab04_InsertionGistSearch
{
    /// <summary>
    /// Анализ алгоритма: время и практическая сложность.
    /// </summary>
    public static class SortingAnalysis
    {
        public static (TimeSpan time, GistInsertionSorter.SortResult stats, double k) Measure(int[] arr)
        {
            int[] copy = (int[])arr.Clone();
            int n = copy.Length;

            var sw = Stopwatch.StartNew();
            var stats = GistInsertionSorter.Sort(copy);
            sw.Stop();

            int ops = stats.Comparisons + stats.Shifts;
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
