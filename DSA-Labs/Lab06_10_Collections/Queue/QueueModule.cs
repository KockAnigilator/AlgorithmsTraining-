using System;
using Lab06_10_Collections.Queue.Interfaces;
using Lab06_10_Collections.Queue.Implementations;
using Lab06_10_Collections.Queue.Services;
using Lab06_10_Collections.Queue.Analysis;

namespace Lab06_10_Collections.Queue
{
    /// <summary>
    /// Модуль демонстрации очередей.
    /// </summary>
    public static class QueueModule
    {
        public static void Run()
        {
            Console.WriteLine("Выберите реализацию очереди:");
            Console.WriteLine("1 — ArrayQueue");
            Console.WriteLine("2 — LinkedQueue");
            Console.Write("Ваш выбор: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            IQueue<int> queue;
            if (choice == 2)
                queue = new LinkedQueue<int>();
            else
                queue = new ArrayQueue<int>();

            var service = new QueueService<int>();
            var rnd = new Random();

            Console.Write("Количество Enqueue: ");
            int enq = ReadInt(1000);

            Console.Write("Количество Dequeue: ");
            int deq = ReadInt(500);

            Action work = () =>
            {
                service.RunOps(queue, enq, deq, () => rnd.Next(1, 100000));
            };

            int ops = enq + Math.Min(enq, deq);

            var result = QueueAnalyzer.Measure(work, ops);

            Console.WriteLine("\n--- Анализ очереди ---");
            Console.WriteLine("Теоретическая сложность: O(1)");
            Console.WriteLine("Практическая сложность: ~k · 1");
            Console.WriteLine("k = " + result.Item2.ToString("F4"));
            Console.WriteLine("Время: " + result.Item1.TotalMilliseconds.ToString("F4") + " мс");

            Console.WriteLine("\nСодержимое очереди:");
            service.Print(queue);

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
