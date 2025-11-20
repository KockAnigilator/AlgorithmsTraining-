using System;
using System.Diagnostics;

namespace Lab06_10_Collections.List.Analysis
{
    /// <summary>
    /// Анализатор операций списков: замер времени и вычисление практической сложности.
    /// </summary>
    public static class ListAnalyzer
    {
        /// <summary>
        /// Выполняет action и возвращает (elapsed, k),
        /// где k = ops / opsUnit. Для операций на концах opsUnit = 1, поэтому вывод ~k · 1.
        /// </summary>
        public static Tuple<TimeSpan, double> Measure(Action action, int ops)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();

            double k = (ops > 0) ? (double)ops / ops : 1.0;
            return Tuple.Create(sw.Elapsed, k);
        }
    }
}
