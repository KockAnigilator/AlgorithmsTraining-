using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11_PipelineOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            var optimizer = new PipelineOptimizer();

            // Добавляем скважины с произвольными координатами
            optimizer.AddWell(1, 3);
            optimizer.AddWell(2, 8);
            optimizer.AddWell(3, 5);
            optimizer.AddWell(4, 12);
            optimizer.AddWell(5, 7);
            optimizer.AddWell(6, 1);
            optimizer.AddWell(7, 9);

            // Получаем решение
            Console.WriteLine(optimizer.GetSolutionInfo());

            // Проверяем с другими координатами
            var optimizer2 = new PipelineOptimizer();
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                optimizer2.AddWell(i + 1, rnd.Next(1, 20));
            }

            Console.WriteLine("\n" + optimizer2.GetSolutionInfo());
        }
    }
}
