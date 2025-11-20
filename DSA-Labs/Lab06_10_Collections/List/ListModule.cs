using System;
using Lab06_10_Collections.List.Interfaces;
using Lab06_10_Collections.List.Implementations;
using Lab06_10_Collections.List.Services;
using Lab06_10_Collections.List.Analysis;

namespace Lab06_10_Collections.List
{
    /// <summary>
    /// Модуль демонстрации List (Singly/Doubly).
    /// </summary>
    public static class ListModule
    {
        public static void Run()
        {
            Console.WriteLine("Выберите реализацию списка:");
            Console.WriteLine("1 — SinglyLinkedList");
            Console.WriteLine("2 — DoublyLinkedList");
            Console.Write("Ваш выбор: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            ILinkedList<int> list = (choice == 2)
                ? (ILinkedList<int>)new DoublyLinkedList<int>()
                : (ILinkedList<int>)new SinglyLinkedList<int>();

            var service = new ListService<int>();
            var rnd = new Random();

            Console.Write("AddFirst count: ");
            int af = ReadInt(100);
            Console.Write("AddLast count: ");
            int al = ReadInt(100);
            Console.Write("RemoveFirst count: ");
            int rf = ReadInt(50);
            Console.Write("RemoveLast count: ");
            int rl = ReadInt(50);

            Action work = () =>
            {
                service.RunOps(list, af, al, rf, rl, () => rnd.Next(1, 100000));
            };

            int ops = af + al + Math.Min(rf, af + al) + Math.Min(rl, Math.Max(0, af + al - rf));

            var result = ListAnalyzer.Measure(work, ops);

            Console.WriteLine("\n--- Анализ списка ---");
            Console.WriteLine("Теоретическая сложность: Add/Remove at ends — O(1); Remove(value)/Contains — O(n)");
            Console.WriteLine("Вызовов (фактически): " + ops);
            Console.WriteLine("Время: " + result.Item1.TotalMilliseconds.ToString("F4") + " ms");
            Console.WriteLine("Практическая сложность: ~" + result.Item2.ToString("F4") + " · 1");

            Console.WriteLine("\nПечать списка (пример):");
            service.Print(list);

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static int ReadInt(int def)
        {
            int v;
            if (!int.TryParse(Console.ReadLine(), out v)) return def;
            return v;
        }
    }
}
