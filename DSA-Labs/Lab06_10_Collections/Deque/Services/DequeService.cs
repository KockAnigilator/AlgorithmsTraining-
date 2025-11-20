using System;
using System.Collections.Generic;
using Lab06_10_Collections.Deque.Interfaces;

namespace Lab06_10_Collections.Deque.Services
{
    public class DequeService<T>
    {
        public void FillRandom(IDeque<T> deque, int n, Func<T> gen)
        {
            for (int i = 0; i < n; i++)
                deque.AddBack(gen());
        }

        public void Print(IDeque<T> deque)
        {
            List<T> temp = new List<T>();

            while (!deque.IsEmpty())
                temp.Add(deque.RemoveFront());

            Console.WriteLine("Содержимое deque:");
            foreach (var x in temp)
                Console.WriteLine(x);

            // восстановление
            foreach (var x in temp)
                deque.AddBack(x);
        }

        public int RunOps(IDeque<T> deque, int addFront, int addBack, int remFront, int remBack, Func<T> gen)
        {
            int ops = 0;

            for (int i = 0; i < addFront; i++) { deque.AddFront(gen()); ops++; }
            for (int i = 0; i < addBack; i++) { deque.AddBack(gen()); ops++; }

            for (int i = 0; i < remFront; i++)
            {
                if (deque.IsEmpty()) break;
                deque.RemoveFront();
                ops++;
            }

            for (int i = 0; i < remBack; i++)
            {
                if (deque.IsEmpty()) break;
                deque.RemoveBack();
                ops++;
            }

            return ops;
        }
    }
}
