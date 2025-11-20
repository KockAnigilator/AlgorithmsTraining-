using System;

namespace Lab05_SearchSortAnalysis.Sorting
{
    /// <summary>
    /// Классическая пузырьковая сортировка (O(n²)).
    /// </summary>
    public static class BubbleSorter
    {
        public struct SortResult
        {
            public int Comparisons;
            public int Swaps;
        }

        public static SortResult Sort(int[] array)
        {
            var r = new SortResult();
            int n = array.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    r.Comparisons++;
                    if (array[j] > array[j + 1])
                    {
                        int t = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = t;
                        r.Swaps++;
                    }
                }
            }

            return r;
        }
    }
}
