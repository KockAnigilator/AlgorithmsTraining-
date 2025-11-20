using System;
using System.Diagnostics;

namespace Lab06_10_Collections.Queue.Analysis
{
    /// <summary>
    /// Анализатор производительности очереди.
    /// </summary>
    public static class QueueAnalyzer
    {
        public static Tuple<TimeSpan, double> Measure(Action action, int ops)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();

            // Теоретическая сложность: O(1)
            double k = (double)ops / ops;

            return Tuple.Create(sw.Elapsed, k);
        }
    }
}
