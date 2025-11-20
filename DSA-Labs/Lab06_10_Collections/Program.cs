using System;
using Lab06_10_Collections.Stack;
using Lab06_10_Collections.Queue;
using Lab06_10_Collections.List;
using Lab06_10_Collections.Deque;

namespace Lab06_10_Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ДЕМОНСТРАЦИЯ КОЛЛЕКЦИЙ ===");
                Console.WriteLine("1 - Работа со стеком (Stack)");
                Console.WriteLine("2 - Работа с очередью (Queue)");
                Console.WriteLine("3 - Работа со списком (List)");
                Console.WriteLine("4 - Работа с деком (Deque)");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите опцию: ");

                var input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Неверный ввод! Нажмите любую клавишу...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        StackModule.Run();
                        break;
                    case 2:
                        QueueModule.Run();
                        break;
                    case 3:
                        ListModule.Run();
                        break;
                    case 4:
                        DequeModule.Run();
                        break;
                    case 0:
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}