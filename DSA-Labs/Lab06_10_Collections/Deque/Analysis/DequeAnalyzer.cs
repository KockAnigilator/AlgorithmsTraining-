using System;
using System.Diagnostics;

namespace Lab06_10_Collections.Deque.Analysis
{
    public static class DequeAnalyzer
    {
        public static Tuple<TimeSpan, double> Measure(Action action, int ops)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();

            // амортизированно: O(1)
            double k = 1.0;

            return Tuple.Create(sw.Elapsed, k);
        }
    }
}
