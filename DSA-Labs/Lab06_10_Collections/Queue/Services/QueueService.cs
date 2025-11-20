using System;
using System.Collections.Generic;
using Lab06_10_Collections.Queue.Interfaces;

namespace Lab06_10_Collections.Queue.Services
{
    /// <summary>
    /// Сервис для работы с очередями: заполнение, вывод, выполнение операций.
    /// </summary>
    public class QueueService<T>
    {
        public void FillRandom(IQueue<T> queue, int count, Func<T> generator)
        {
            for (int i = 0; i < count; i++)
                queue.Enqueue(generator());
        }

        public void Print(IQueue<T> queue)
        {
            List<T> temp = new List<T>();

            while (!queue.IsEmpty())
                temp.Add(queue.Dequeue());

            Console.WriteLine("Содержимое очереди (от головы к хвосту):");
            foreach (var e in temp)
                Console.WriteLine(e);

            foreach (var e in temp)
                queue.Enqueue(e);
        }

        public int RunOps(IQueue<T> queue, int enqueueCount, int dequeueCount, Func<T> generator)
        {
            int total = 0;

            for (int i = 0; i < enqueueCount; i++)
            {
                queue.Enqueue(generator());
                total++;
            }

            for (int i = 0; i < dequeueCount; i++)
            {
                if (queue.IsEmpty()) break;
                queue.Dequeue();
                total++;
            }

            return total;
        }
    }
}
