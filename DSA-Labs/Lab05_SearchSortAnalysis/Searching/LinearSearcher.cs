namespace Lab05_SearchSortAnalysis.Searching
{
    /// <summary>
    /// Линейный поиск (O(n)).
    /// </summary>
    public static class LinearSearcher
    {
        public struct SearchResult
        {
            public int Comparisons;
            public int Index;
        }

        public static SearchResult Search(int[] array, int value)
        {
            var r = new SearchResult();

            for (int i = 0; i < array.Length; i++)
            {
                r.Comparisons++;
                if (array[i] == value)
                {
                    r.Index = i;
                    return r;
                }
            }

            r.Index = -1;
            return r;
        }
    }
}
