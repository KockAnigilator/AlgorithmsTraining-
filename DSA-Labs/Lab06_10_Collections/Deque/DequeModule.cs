using System;
using Lab06_10_Collections.Deque.Interfaces;
using Lab06_10_Collections.Deque.Implementations;
using Lab06_10_Collections.Deque.Services;
using Lab06_10_Collections.Deque.Analysis;

namespace Lab06_10_Collections.Deque
{
    /// <summary>
    /// Модуль демонстрации Deque.
    /// </summary>
    public static class DequeModule
    {
        public static void Run()
        {
            Console.WriteLine("Выберите реализацию deque:");
            Console.WriteLine("1 — ArrayDeque");
            Console.WriteLine("2 — LinkedDeque");
            Console.Write("Ваш выбор: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            IDeque<int> deq = (choice == 2)
                ? (IDeque<int>)new LinkedDeque<int>()
                : (IDeque<int>)new ArrayDeque<int>();

            var service = new DequeService<int>();
            var rnd = new Random();

            Console.Write("AddFront: ");
            int af = ReadInt(500);

            Console.Write("AddBack: ");
            int ab = ReadInt(500);

            Console.Write("RemoveFront: ");
            int rf = ReadInt(300);

            Console.Write("RemoveBack: ");
            int rb = ReadInt(300);

            Action work = () =>
            {
                service.RunOps(deq, af, ab, rf, rb, () => rnd.Next(1, 100000));
            };

            int ops = af + ab + Math.Min(rf, af + ab) + Math.Min(rb, af + ab);

            var result = DequeAnalyzer.Measure(work, ops);

            Console.WriteLine("\n--- Анализ deque ---");
            Console.WriteLine("Теоретическая сложность: O(1)");
            Console.WriteLine("Практическая: ~k · 1");
            Console.WriteLine("Время: " + result.Item1.TotalMilliseconds.ToString("F4") + " мс");

            Console.WriteLine("\nСодержимое deque:");
            service.Print(deq);

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private static int ReadInt(int def)
        {
            int x;
            return int.TryParse(Console.ReadLine(), out x) ? x : def;
        }
    }
}
