namespace Lab01_Matrix
{
    /// <summary>
    /// Прямоугольная матрица произвольного размера.
    /// </summary>
    public sealed class RectangularMatrix : Matrix
    {
        /// <summary>
        /// Создаёт прямоугольную матрицу указанного размера.
        /// </summary>
        public RectangularMatrix(int rows, int cols) : base(rows, cols)
        {
        }

        /// <summary>
        /// Создаёт матрицу и заполняет её указанным значением.
        /// </summary>
        public RectangularMatrix(int rows, int cols, double initialValue)
            : base(rows, cols)
        {
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Cols; c++)
                    this[r, c] = initialValue;
        }
    }
}
