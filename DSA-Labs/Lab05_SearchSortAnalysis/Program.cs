using System;
using Lab05_SearchSortAnalysis.Sorting;
using Lab05_SearchSortAnalysis.Searching;
using Lab05_SearchSortAnalysis.Analysis;
using Lab03_ShakerSort;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите массив через пробел:");
        string[] parts = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] array = Array.ConvertAll(parts, int.Parse);

        Console.WriteLine("\nВыберите алгоритм сортировки:");
        Console.WriteLine("1 — Bubble Sort");
        Console.WriteLine("2 — Insertion Sort");
        Console.WriteLine("3 — Binary Insertion Sort");
        Console.WriteLine("4 — Shaker Sort");
        Console.Write("Ваш выбор: ");

        int choice = int.Parse(Console.ReadLine());

        Func<int[], object> sorter;

        
        switch (choice)
        {
            case 1:
                sorter = a => BubbleSorter.Sort(a);
                break;
            case 2:
                sorter = a => InsertionSorter.Sort(a);
                break;
            case 3:
                sorter = a => BinaryInsertionSorter.Sort(a);
                break;
            case 4:
                sorter = a => ShakerSorter.Sort(a); // из Лабы 3
                break;
            default:
                sorter = a => BubbleSorter.Sort(a);
                break;
        }

        Console.WriteLine("\nИсходный массив:");
        Console.WriteLine(string.Join(", ", array));

        object stats;
        double k;
        TimeSpan time = SortAnalysis.MeasureTime(sorter, array, out stats, out k);

        // Финальная сортировка для вывода
        sorter(array);

        Console.WriteLine("\nОтсортированный массив:");
        Console.WriteLine(string.Join(", ", array));

        Console.WriteLine("\nРезультаты анализа:");

        var t = stats.GetType();

        var fComparisons = t.GetField("Comparisons");
        var fSwaps = t.GetField("Swaps");
        var fShifts = t.GetField("Shifts");

        if (fComparisons != null)
            Console.WriteLine("Сравнений:               " + fComparisons.GetValue(stats));

        if (fSwaps != null)
            Console.WriteLine("Обменов:                 " + fSwaps.GetValue(stats));

        if (fShifts != null)
            Console.WriteLine("Сдвигов:                 " + fShifts.GetValue(stats));

        int ops =
            (fComparisons != null ? (int)fComparisons.GetValue(stats) : 0) +
            (fSwaps != null ? (int)fSwaps.GetValue(stats) : 0) +
            (fShifts != null ? (int)fShifts.GetValue(stats) : 0);

        Console.WriteLine("Операций всего:          " + ops);

        Console.WriteLine("Практическая сложность:  ~" + k.ToString("F4") + " · n^2");
        Console.WriteLine("Время выполнения:        " + time.TotalMilliseconds.ToString("F4") + " мс");

        Console.ReadKey();
    }
}
