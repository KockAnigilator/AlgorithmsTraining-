using System;
using System.Diagnostics;

namespace Lab05_SearchSortAnalysis.Analysis
{
    public static class SortAnalysis
    {
        public static (TimeSpan time, dynamic stats, double k) Measure<T>(Func<int[], T> sorter, int[] arr)
        {
            int[] copy = (int[])arr.Clone();
            int n = copy.Length;

            Stopwatch sw = Stopwatch.StartNew();
            var stats = sorter(copy);
            sw.Stop();

            int ops = 0;

            if (stats is { })
            {
                var t = stats.GetType();

                if (t.GetField("Comparisons") != null)
                    ops += (int)t.GetField("Comparisons").GetValue(stats);

                if (t.GetField("Swaps") != null)
                    ops += (int)t.GetField("Swaps").GetValue(stats);

                if (t.GetField("Shifts") != null)
                    ops += (int)t.GetField("Shifts").GetValue(stats);
            }

            double k = (double)ops / (n * n);

            return (sw.Elapsed, stats, k);
        }
    }
}
