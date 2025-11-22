using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab11_PipelineOptimization
{
    public class PipelineOptimizer
    {
        private List<Well> _wells;

        public PipelineOptimizer()
        {
            _wells = new List<Well>();
        }

        public void AddWell(double x, double y)
        {
            int id = _wells.Count + 1;
            _wells.Add(new Well(id, x, y));
        }

        public void AddWell(Well well)
        {
            _wells.Add(well);
        }

        /// <summary>
        /// Находит оптимальное положение трубопровода (медиану y-координат)
        /// </summary>
        public double FindOptimalPipelineHeight()
        {
            if (_wells.Count == 0)
                throw new InvalidOperationException("Нет скважин для анализа");

            var yCoordinates = _wells.Select(w => w.Y).ToArray();
            return FindMedian(yCoordinates);
        }

        /// <summary>
        /// Вычисляет суммарную длину труб при заданной высоте магистрали
        /// </summary>
        public double CalculateTotalPipeLength(double pipelineHeight)
        {
            return _wells.Sum(well => Math.Abs(well.Y - pipelineHeight));
        }

        /// <summary>
        /// Находит медиану массива за O(n) в среднем случае
        /// </summary>
        private double FindMedian(double[] array)
        {
            if (array.Length == 0) return 0;

            int k = array.Length / 2;

            if (array.Length % 2 == 0)
            {
                // Для четного количества - среднее двух центральных элементов
                double median1 = QuickSelect(array, k - 1, 0, array.Length - 1);
                double median2 = QuickSelect(array, k, 0, array.Length - 1);
                return (median1 + median2) / 2.0;
            }
            else
            {
                // Для нечетного количества - центральный элемент
                return QuickSelect(array, k, 0, array.Length - 1);
            }
        }

        /// <summary>
        /// Алгоритм быстрого выбора для нахождения k-го наименьшего элемента
        /// </summary>
        private double QuickSelect(double[] array, int k, int left, int right)
        {
            while (left <= right)
            {
                if (left == right) return array[left];

                int pivotIndex = Partition(array, left, right);

                if (k == pivotIndex)
                {
                    return array[k];
                }
                else if (k < pivotIndex)
                {
                    right = pivotIndex - 1;
                }
                else
                {
                    left = pivotIndex + 1;
                }
            }

            return array[k];
        }

        /// <summary>
        /// Разделение массива для быстрого выбора
        /// </summary>
        private int Partition(double[] array, int left, int right)
        {
            double pivot = array[right];
            int i = left;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    Swap(array, i, j);
                    i++;
                }
            }

            Swap(array, i, right);
            return i;
        }

        private void Swap(double[] array, int i, int j)
        {
            double temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        /// <summary>
        /// Получает информацию о скважинах и оптимальном решении
        /// </summary>
        public string GetSolutionInfo()
        {
            if (_wells.Count == 0)
                return "Нет данных о скважинах";

            double optimalHeight = FindOptimalPipelineHeight();
            double totalLength = CalculateTotalPipeLength(optimalHeight);

            var info = new System.Text.StringBuilder();
            info.AppendLine("=== ОПТИМАЛЬНОЕ РАСПОЛОЖЕНИЕ ТРУБОПРОВОДА ===");
            info.AppendLine($"Количество скважин: {_wells.Count}");
            info.AppendLine($"Оптимальная высота магистрали: {optimalHeight:F2}");
            info.AppendLine($"Суммарная длина перпендикулярных труб: {totalLength:F2}");
            info.AppendLine();
            info.AppendLine("Координаты скважин:");

            foreach (var well in _wells.OrderBy(w => w.X))
            {
                info.AppendLine($"  Скважина {well.Id}: ({well.X:F1}, {well.Y:F1}) -> длина: {Math.Abs(well.Y - optimalHeight):F2}");
            }

            return info.ToString();
        }
    }
}
