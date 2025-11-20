using System;

namespace Lab03_ShakerSort
{
    /// <summary>
    /// Реализация алгоритма шейкер-сортировки.
    /// </summary>
    public static class ShakerSorter
    {
        /// <summary>
        /// Статистика сортировки: количество сравнений и обменов.
        /// </summary>
        public struct SortResult
        {
            public int Comparisons;
            public int Swaps;
        }

        /// <summary>
        /// Выполняет шейкер-сортировку массива по возрастанию.
        /// </summary>
        public static SortResult Sort(int[] array)
        {
            var r = new SortResult();
            int left = 0;
            int right = array.Length - 1;
            bool swapped;

            do
            {
                swapped = false;

                // Проход слева направо
                for (int i = left; i < right; i++)
                {
                    r.Comparisons++;
                    if (array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                        r.Swaps++;
                        swapped = true;
                    }
                }
                right--;
                if (!swapped) break;

                swapped = false;

                // Проход справа налево
                for (int i = right; i > left; i--)
                {
                    r.Comparisons++;
                    if (array[i - 1] > array[i])
                    {
                        Swap(array, i - 1, i);
                        r.Swaps++;
                        swapped = true;
                    }
                }
                left++;

            } while (swapped);

            return r;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            int t = arr[i];
            arr[i] = arr[j];
            arr[j] = t;
        }
    }
}
