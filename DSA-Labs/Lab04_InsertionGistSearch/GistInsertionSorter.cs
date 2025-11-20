using System;

namespace Lab04_InsertionGistSearch
{
    /// <summary>
    /// Реализация модифицированной сортировки вставками
    /// (jump search + binary search).
    /// </summary>
    public static class GistInsertionSorter
    {
        /// <summary>
        /// Статистика алгоритма: сравнения и сдвиги.
        /// </summary>
        public struct SortResult
        {
            public int Comparisons;
            public int Shifts;
        }

        /// <summary>
        /// Выполняет сортировку массива.
        /// </summary>
        public static SortResult Sort(int[] a)
        {
            var r = new SortResult();

            for (int i = 1; i < a.Length; i++)
            {
                int value = a[i];
                int left = 0;
                int right = i - 1;

                // Jump-search
                int step = (int)Math.Sqrt(i);
                int p = right;

                while (p >= 0)
                {
                    r.Comparisons++;
                    if (a[p] <= value)
                    {
                        left = p;
                        break;
                    }
                    p -= step;
                }
                if (p < 0) left = 0;

                // Binary search
                int l = left;
                int rgt = right;
                while (l <= rgt)
                {
                    int mid = (l + rgt) / 2;
                    r.Comparisons++;
                    if (a[mid] <= value)
                        l = mid + 1;
                    else
                        rgt = mid - 1;
                }

                int pos = l;

                // Shift
                for (int j = i; j > pos; j--)
                {
                    a[j] = a[j - 1];
                    r.Shifts++;
                }
                a[pos] = value;
            }

            return r;
        }
    }
}
