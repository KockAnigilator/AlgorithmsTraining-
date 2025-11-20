using System;
using Lab06_10_Collections.Queue.Interfaces;

namespace Lab06_10_Collections.Queue.Implementations
{
    /// <summary>
    /// Очередь на основе односвязного списка: head → tail.
    /// </summary>
    public class LinkedQueue<T> : IQueue<T>
    {
        private Node _head;
        private Node _tail;
        private int _count;

        private sealed class Node
        {
            public T Value;
            public Node Next;
            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        public int Count { get { return _count; } }

        public void Enqueue(T item)
        {
            var node = new Node(item);

            if (_tail == null)
            {
                _head = _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail = node;
            }

            _count++;
        }

        public T Dequeue()
        {
            if (_head == null)
                throw new InvalidOperationException("Queue is empty.");

            T val = _head.Value;
            _head = _head.Next;
            if (_head == null) _tail = null;

            _count--;
            return val;
        }

        public T Peek()
        {
            if (_head == null)
                throw new InvalidOperationException("Queue is empty.");
            return _head.Value;
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }
    }
}
