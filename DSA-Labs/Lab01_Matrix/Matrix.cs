using System;

namespace Lab01_Matrix
{
    /// <summary>
    /// Абстрактный базовый класс для работы с матрицами.
    /// Содержит общие операции: индексатор, транспонирование, сложение,
    /// умножение матриц, конвертация в массив и обратно.
    /// </summary>
    public abstract class Matrix
    {
        /// <summary>
        /// Двумерный массив, хранящий элементы матрицы.
        /// </summary>
        protected readonly double[,] data;

        /// <summary>Количество строк матрицы.</summary>
        public int Rows { get; }

        /// <summary>Количество столбцов матрицы.</summary>
        public int Cols { get; }

        /// <summary>
        /// Создаёт матрицу заданного размера.
        /// </summary>
        public Matrix(int rows, int cols)
        {
            if (rows <= 0) throw new ArgumentOutOfRangeException(nameof(rows), "Количество строк должно быть > 0.");
            if (cols <= 0) throw new ArgumentOutOfRangeException(nameof(cols), "Количество столбцов должно быть > 0.");

            Rows = rows;
            Cols = cols;
            data = new double[rows, cols];
        }

        /// <summary>
        /// Индексатор для доступа к элементам матрицы.
        /// Позволяет читать и записывать значения: matrix[r, c].
        /// </summary>
        public double this[int r, int c]
        {
            get
            {
                ValidateIndices(r, c);
                return data[r, c];
            }
            set
            {
                ValidateIndices(r, c);
                data[r, c] = value;
            }
        }

        /// <summary>
        /// Проверяет допустимость индексов строки и столбца.
        /// </summary>
        private void ValidateIndices(int r, int c)
        {
            if (r < 0 || r >= Rows) throw new IndexOutOfRangeException("Недопустимый индекс строки.");
            if (c < 0 || c >= Cols) throw new IndexOutOfRangeException("Недопустимый индекс столбца.");
        }

        // ─────────────────────────────────────────────────────────────────────────────
        //   КОНВЕРТАЦИЯ МЕЖДУ МАССИВОМ И МАТРИЦЕЙ
        // ─────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Создаёт матрицу из одномерного массива по строкам.
        /// arr = {a11, a12, ..., a1n, a21, ..., a2n, ...}
        /// </summary>
        public static RectangularMatrix FromArray(double[] arr, int rows, int cols)
        {
            if (arr == null)
                throw new ArgumentNullException(nameof(arr));

            if (rows * cols != arr.Length)
                throw new ArgumentException("Размер массива не совпадает с размером матрицы.");

            var m = new RectangularMatrix(rows, cols);

            for (int i = 0; i < arr.Length; i++)
            {
                int r = i / cols;
                int c = i % cols;
                m[r, c] = arr[i];
            }

            return m;
        }

        /// <summary>
        /// Преобразует матрицу в одномерный массив, записывая элементы по строкам.
        /// </summary>
        public double[] ToArray()
        {
            var result = new double[Rows * Cols];
            int index = 0;

            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    result[index++] = data[r, c];

            return result;
        }

        // ─────────────────────────────────────────────────────────────────────────────
        //   ОПЕРАЦИИ НАД МАТРИЦАМИ
        // ─────────────────────────────────────────────────────────────────────────────

        /// <summary>
        /// Создаёт и возвращает транспонированную копию матрицы.
        /// Меняет строки и столбцы местами.
        /// </summary>
        public Matrix Transpose()
        {
            var result = new RectangularMatrix(Cols, Rows);

            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    result[c, r] = data[r, c];

            return result;
        }

        /// <summary>
        /// Умножает матрицу на число и возвращает новую матрицу.
        /// </summary>
        public Matrix MultiplyByScalar(double k)
        {
            var result = new RectangularMatrix(Rows, Cols);

            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    result[r, c] = data[r, c] * k;

            return result;
        }

        /// <summary>
        /// Складывает две матрицы одинакового размера и возвращает результат.
        /// </summary>
        public Matrix Add(Matrix other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            if (Rows != other.Rows || Cols != other.Cols)
                throw new ArgumentException("Матрицы разных размеров нельзя складывать.");

            var result = new RectangularMatrix(Rows, Cols);

            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    result[r, c] = data[r, c] + other[r, c];

            return result;
        }

        /// <summary>
        /// Перемножает текущую матрицу с другой (A × B).
        /// Условие: число столбцов A = числу строк B.
        /// </summary>
        public Matrix Multiply(Matrix other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (Cols != other.Rows)
                throw new ArgumentException("Несовместимые размеры матриц.");

            var result = new RectangularMatrix(Rows, other.Cols);

            // Формула:
            //   result[i, j] = Σ A[i,k] * B[k,j],   k = 0..Cols-1
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < other.Cols; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < Cols; k++)
                        sum += data[i, k] * other[k, j];

                    result[i, j] = sum;
                }
            }

            return result;
        }

        /// <summary>
        /// Красивый вывод матрицы в текстовом формате.
        /// </summary>
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            for (int r = 0; r < Rows; r++)
            {
                sb.Append("[ ");
                for (int c = 0; c < Cols; c++)
                {
                    sb.AppendFormat("{0,8:0.###}", data[r, c]);
                    if (c + 1 < Cols) sb.Append(", ");
                }
                sb.Append(" ]");

                if (r + 1 < Rows) sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
