namespace Lab05_SearchSortAnalysis.Searching
{
    /// <summary>
    /// Бинарный поиск (O(log n)).
    /// </summary>
    public static class BinarySearcher
    {
        public struct SearchResult
        {
            public int Comparisons;
            public int Index;
        }

        public static SearchResult Search(int[] array, int value)
        {
            var r = new SearchResult();
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;
                r.Comparisons++;

                if (array[mid] == value)
                {
                    r.Index = mid;
                    return r;
                }
                else if (array[mid] < value)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            r.Index = -1;
            return r;
        }
    }
}
