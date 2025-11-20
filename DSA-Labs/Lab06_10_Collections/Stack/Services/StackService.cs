using System;
using System.Collections.Generic;
using Lab06_10_Collections.Stack.Interfaces;

namespace Lab06_10_Collections.Stack.Services
{
    /// <summary>
    /// Сервис операций со стеком:
    /// заполнение, печать, выполнение тестовых операций.
    /// </summary>
    public class StackService<T>
    {
        /// <summary>
        /// Заполняет стек случайными значениями через генератор.
        /// </summary>
        public void FillRandom(IStack<T> stack, int count, Func<T> generator)
        {
            for (int i = 0; i < count; i++)
                stack.Push(generator());
        }

        /// <summary>
        /// Печатает содержимое стека.
        /// Без доступа к коллекции — временно выгружаем элементы и восстанавливаем.
        /// </summary>
        public void Print(IStack<T> stack)
        {
            var temp = new List<T>();

            while (!stack.IsEmpty())
                temp.Add(stack.Pop());

            Console.WriteLine("Содержимое стека (с вершины вниз):");

            for (int i = 0; i < temp.Count; i++)
                Console.WriteLine(temp[i]);

            for (int i = temp.Count - 1; i >= 0; i--)
                stack.Push(temp[i]);
        }

        /// <summary>
        /// Выполняет pushCount Push и popCount Pop.
        /// Возвращает количество выполненных операций.
        /// </summary>
        public int RunOps(IStack<T> stack, int pushCount, int popCount, Func<T> generator)
        {
            int total = 0;

            for (int i = 0; i < pushCount; i++)
            {
                stack.Push(generator());
                total++;
            }

            for (int i = 0; i < popCount; i++)
            {
                if (stack.IsEmpty()) break;
                stack.Pop();
                total++;
            }

            return total;
        }
    }
}
