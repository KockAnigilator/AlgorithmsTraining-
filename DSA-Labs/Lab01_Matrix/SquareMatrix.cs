using System;

namespace Lab01_Matrix
{
    /// <summary>
    /// Модель квадратной матрицы размера N × N.
    /// Содержит дополнительную операцию — вычисление детерминанта.
    /// </summary>
    public sealed class SquareMatrix : Matrix
    {
        /// <summary>Размер квадратной матрицы.</summary>
        public int Size => Rows;

        public SquareMatrix(int size) : base(size, size)
        {
        }

        /// <summary>
        /// Вычисляет детерминант с помощью приведения к верхнетреугольному виду.
        /// Метод работает для небольших матриц (N ≤ ~20).
        /// </summary>
        public double Determinant()
        {
            int n = Size;
            double[,] a = new double[n, n];

            // Копируем исходную матрицу, чтобы не портить data.
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = this[i, j];

            double det = 1.0;

            for (int i = 0; i < n; i++)
            {
                // Ищем ненулевой элемент на диагонали
                int pivot = i;
                while (pivot < n && Math.Abs(a[pivot, i]) < 1e-12)
                    pivot++;

                if (pivot == n)
                    return 0; // столбец нулевой → детерминант = 0

                // Если нашли строку ниже — меняем местами
                if (pivot != i)
                {
                    for (int j = i; j < n; j++)
                    {
                        double temp = a[i, j];
                        a[i, j] = a[pivot, j];
                        a[pivot, j] = temp;
                    }
                    det = -det; // смена строки меняет знак детерминанта
                }

                double pivotValue = a[i, i];
                det *= pivotValue;

                if (Math.Abs(pivotValue) < 1e-12)
                    return 0;

                // Обнуляем элементы ниже диагонали
                for (int r = i + 1; r < n; r++)
                {
                    double factor = a[r, i] / pivotValue;
                    for (int c = i; c < n; c++)
                        a[r, c] -= factor * a[i, c];
                }
            }

            return det;
        }
    }
}
