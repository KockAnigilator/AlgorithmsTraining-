using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11_PipelineOptimization
{
    public class PipelineVisualizer
    {
        public static void VisualizeSolution(List<Well> wells, double pipelineHeight)
        {
            Console.WriteLine("ВИЗУАЛИЗАЦИЯ РАСПОЛОЖЕНИЯ:");
            Console.WriteLine("(E - восток, W - запад, * - скважина, = - трубопровод)");
            Console.WriteLine();

            int minY = (int)wells.Min(w => w.Y) - 1;
            int maxY = (int)wells.Max(w => w.Y) + 1;
            int pipelineY = (int)pipelineHeight;

            for (int y = maxY; y >= minY; y--)
            {
                if (y == pipelineY)
                {
                    Console.Write("E");
                    for (int i = 0; i < 20; i++) Console.Write("=");
                    Console.WriteLine("W");
                }
                else
                {
                    Console.Write(" ");
                    foreach (var well in wells.OrderBy(w => w.X))
                    {
                        if ((int)well.Y == y)
                        {
                            Console.Write(" * ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
