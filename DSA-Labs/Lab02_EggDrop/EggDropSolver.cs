using System;

namespace Lab02_EggDrop
{
    /// <summary>
    /// Решает классическую задачу о двух шарах (двух яйцах).
    /// Вычисляет минимальное число попыток, достаточное в худшем случае
    /// для определения "критического" этажа среди N.
    /// </summary>
    public static class EggDropSolver
    {
        /// <summary>
        /// Вычисляет минимальное число бросков для двух шаров и N этажей.
        /// Оптимальная стратегия — последовательность уменьшающихся шагов.
        /// </summary>
        /// <param name="floors">Количество этажей здания N.</param>
        /// <returns>Минимальное число бросков в худшем случае.</returns>
        public static int MinAttempts(int floors)
        {
            if (floors <= 0)
                return 0;

            // Формула:
            //   X*(X+1)/2 ≥ floors
            // Решение квадратного уравнения даёт:
            //   X = ceil( (sqrt(1 + 8*N) - 1) / 2 )
            double x =
                (Math.Sqrt(1 + 8.0 * floors) - 1.0) / 2.0;

            return (int)Math.Ceiling(x);
        }

        /// <summary>
        /// Симулирует последовательность этажей,
        /// с которых нужно бросать шар на каждом шаге.
        /// </summary>
        public static int[] BuildStrategy(int floors)
        {
            int attempts = MinAttempts(floors);
            int[] sequence = new int[attempts];

            int current = 0;
            int step = attempts;

            // Формируем последовательность:
            // 1-й бросок: step
            // 2-й бросок: step + (step - 1)
            // ...
            for (int i = 0; i < attempts; i++)
            {
                current += step;
                if (current > floors)
                    current = floors;

                sequence[i] = current;
                step--;
            }

            return sequence;
        }
    }
}
