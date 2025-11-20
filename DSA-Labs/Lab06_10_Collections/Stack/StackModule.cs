using System;
using Lab06_10_Collections.Stack.Interfaces;
using Lab06_10_Collections.Stack.Implementations;
using Lab06_10_Collections.Stack.Services;
using Lab06_10_Collections.Stack.Analysis;

namespace Lab06_10_Collections.Stack
{
    /// <summary>
    /// Модуль демонстрации и анализа работы стека.
    /// Вызывается из Program.cs через switch.
    /// </summary>
    public static class StackModule
    {
        public static void Run()
        {
            Console.WriteLine("Выберите реализацию стека:");
            Console.WriteLine("1 — ArrayStack");
            Console.WriteLine("2 — LinkedStack");
            Console.Write("Ваш выбор: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            IStack<int> stack;

            if (choice == 2)
                stack = new LinkedStack<int>();
            else
                stack = new ArrayStack<int>();

            var service = new StackService<int>();
            var rnd = new Random();

            Console.Write("Сколько Push выполнить? ");
            int pushCount = ReadInt(1000);

            Console.Write("Сколько Pop выполнить? ");
            int popCount = ReadInt(500);

            Action work = () =>
            {
                service.RunOps(stack, pushCount, popCount, () => rnd.Next(1, 10000));
            };

            int highOps = pushCount + Math.Min(pushCount, popCount);

            var result = StackAnalyzer.Measure(work, highOps);

            Console.WriteLine("\n--- Анализ стека ---");
            Console.WriteLine("Теоретическая сложность операций: O(1)");
            Console.WriteLine("Практическая сложность: ~k · 1");
            Console.WriteLine("k = " + result.Item2.ToString("F4"));
            Console.WriteLine("Время: " + result.Item1.TotalMilliseconds.ToString("F4") + " мс");

            Console.WriteLine("\nСодержимое стека после операций:");
            service.Print(stack);

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private static int ReadInt(int def)
        {
            int v;
            if (int.TryParse(Console.ReadLine(), out v)) return v;
            return def;
        }
    }
}
