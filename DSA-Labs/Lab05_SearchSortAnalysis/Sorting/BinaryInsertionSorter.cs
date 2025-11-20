using System;

namespace Lab05_SearchSortAnalysis.Sorting
{
    /// <summary>
    /// Сортировка вставками с бинарным поиском позиции вставки.
    /// </summary>
    public static class BinaryInsertionSorter
    {
        public struct SortResult
        {
            public int Comparisons;
            public int Shifts;
        }

        public static SortResult Sort(int[] array)
        {
            var r = new SortResult();

            for (int i = 1; i < array.Length; i++)
            {
                int value = array[i];

                // Бинарный поиск позиции
                int left = 0;
                int right = i - 1;

                while (left <= right)
                {
                    r.Comparisons++;
                    int mid = (left + right) / 2;

                    if (array[mid] <= value)
                        left = mid + 1;
                    else
                        right = mid - 1;
                }

                int pos = left;

                for (int j = i; j > pos; j--)
                {
                    array[j] = array[j - 1];
                    r.Shifts++;
                }

                array[pos] = value;
            }

            return r;
        }
    }
}
