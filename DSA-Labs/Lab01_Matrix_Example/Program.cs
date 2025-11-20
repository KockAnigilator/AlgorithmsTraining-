using System;

namespace Lab01_Matrix_Example
{
    using Lab01_Matrix;

    class Program
    {
        static void Main(string[] args)
        {
            // Пример: одномерный массив -> матрица (3×3)
            double[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var mat = Matrix.FromArray(arr, 3, 3); // возвращает RectangularMatrix

            Console.WriteLine("Матрица:");
            Console.WriteLine(mat);

            // Транспонирование
            var trans = mat.Transpose();
            Console.WriteLine("\nТранспонированная:");
            Console.WriteLine(trans);

            // Умножение матриц
            var prod = mat.Multiply(trans);
            Console.WriteLine("\nМатрица * Транспонированная:");
            Console.WriteLine(prod);

            // В массив обратно
            var back = prod.ToArray();
            Console.WriteLine("\nОбратно в массив (длина = {0}):", back.Length);
            Console.WriteLine(string.Join(", ", back));

            // Если хотим квадратную матрицу:
            var sq = new SquareMatrix(3);
            sq[0, 0] = 1; sq[0, 1] = 2; sq[0, 2] = 3;
            sq[1, 0] = 0; sq[1, 1] = 4; sq[1, 2] = 5;
            sq[2, 0] = 1; sq[2, 1] = 0; sq[2, 2] = 6;
            Console.WriteLine("\nSquare matrix:");
            Console.WriteLine(sq);
            Console.WriteLine("Determinant: " + sq.Determinant());

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
