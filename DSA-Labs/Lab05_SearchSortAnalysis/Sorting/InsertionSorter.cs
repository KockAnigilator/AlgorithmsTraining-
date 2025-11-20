using System;

namespace Lab05_SearchSortAnalysis.Sorting
{
    /// <summary>
    /// Классическая сортировка вставками (O(n²)).
    /// </summary>
    public static class InsertionSorter
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
                int j = i - 1;

                while (j >= 0)
                {
                    r.Comparisons++;
                    if (array[j] > value)
                    {
                        array[j + 1] = array[j];
                        r.Shifts++;
                        j--;
                    }
                    else break;
                }

                array[j + 1] = value;
            }

            return r;
        }
    }
}
