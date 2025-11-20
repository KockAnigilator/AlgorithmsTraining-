using System;
using System.Collections.Generic;
using Lab06_10_Collections.List.Interfaces;

namespace Lab06_10_Collections.List.Services
{
    /// <summary>
    /// Сервис для операций со списками: заполнение, печать, тестовые операции.
    /// </summary>
    public class ListService<T>
    {
        /// <summary>Заполнить список count элементами (AddLast) через генератор.</summary>
        public void FillRandom(ILinkedList<T> list, int count, Func<T> generator)
        {
            for (int i = 0; i < count; i++)
                list.AddLast(generator());
        }

        /// <summary>Универсальная печать: извлечь все элементы и восстановить.</summary>
        public void Print(ILinkedList<T> list)
        {
            var temp = new List<T>();

            while (!list.IsEmpty())
                temp.Add(list.RemoveFirst());

            Console.WriteLine("Содержимое списка (от первой к последней):");
            foreach (var v in temp) Console.WriteLine(v);

            // восстановление
            for (int i = 0; i < temp.Count; i++) list.AddLast(temp[i]);
        }

        /// <summary>
        /// Выполняет addsFirst AddFirst, addsLast AddLast, removesFirst RemoveFirst, removesLast RemoveLast.
        /// Возвращает количество вызовов (операций).
        /// </summary>
        public int RunOps(ILinkedList<T> list, int addsFirst, int addsLast, int remFirst, int remLast, Func<T> generator)
        {
            int ops = 0;

            for (int i = 0; i < addsFirst; i++) { list.AddFirst(generator()); ops++; }
            for (int i = 0; i < addsLast; i++) { list.AddLast(generator()); ops++; }

            for (int i = 0; i < remFirst; i++)
            {
                if (list.IsEmpty()) break;
                list.RemoveFirst();
                ops++;
            }

            for (int i = 0; i < remLast; i++)
            {
                if (list.IsEmpty()) break;
                list.RemoveLast();
                ops++;
            }

            return ops;
        }
    }
}
