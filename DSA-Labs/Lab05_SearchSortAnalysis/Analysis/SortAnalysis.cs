using System;
using System.Diagnostics;
using System.Reflection;

namespace Lab05_SearchSortAnalysis.Analysis
{
    public static class SortAnalysis
    {
        public static TimeSpan MeasureTime(Func<int[], object> sorter, int[] arr, out object stats, out double k)
        {
            int[] copy = (int[])arr.Clone();
            int n = copy.Length;

            Stopwatch sw = Stopwatch.StartNew();
            stats = sorter(copy); // возвращает struct
            sw.Stop();

            int ops = 0;

            FieldInfo fComparisons = stats.GetType().GetField("Comparisons");
            FieldInfo fSwaps = stats.GetType().GetField("Swaps");
            FieldInfo fShifts = stats.GetType().GetField("Shifts");

            if (fComparisons != null)
                ops += (int)fComparisons.GetValue(stats);

            if (fSwaps != null)
                ops += (int)fSwaps.GetValue(stats);

            if (fShifts != null)
                ops += (int)fShifts.GetValue(stats);

            k = n > 0 ? (double)ops / (n * n) : 0;

            return sw.Elapsed;
        }
    }
}
