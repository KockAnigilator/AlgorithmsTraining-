using System;
using System.Diagnostics;

namespace Lab06_10_Collections.Stack.Analysis
{
    /// <summary>
    /// Специализированный анализатор операций стека.
    /// Использует замер времени + вычисляет практическую сложность.
    /// </summary>
    public static class StackAnalyzer
    {
        /// <summary>
        /// Замеряет время выполнения действия и вычисляет коэффициент k
        /// так, что практическая сложность ≈ k · 1.
        /// </summary>
        public static Tuple<TimeSpan, double> Measure(Action action, int ops)
        {
            var sw = Stopwatch.StartNew();
            action();
            sw.Stop();

            // Теоретическая сложность операций стека — O(1), асимптотика = 1.
            // k — сколько операций в среднем на 1 вызов.
            double k = (double)ops / ops;

            return Tuple.Create(sw.Elapsed, k);
        }
    }
}
