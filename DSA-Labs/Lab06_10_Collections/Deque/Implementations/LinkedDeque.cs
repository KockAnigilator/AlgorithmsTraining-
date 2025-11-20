using System;
using Lab06_10_Collections.Deque.Interfaces;

namespace Lab06_10_Collections.Deque.Implementations
{
    /// <summary>
    /// Двусторонняя очередь на двусвязном списке.
    /// </summary>
    public class LinkedDeque<T> : IDeque<T>
    {
        private Node _head;
        private Node _tail;
        private int _count;

        public int Count { get { return _count; } }

        private sealed class Node
        {
            public T Value;
            public Node Prev;
            public Node Next;

            public Node(T value)
            {
                Value = value;
            }
        }

        public bool IsEmpty() { return _count == 0; }

        public void AddFront(T item)
        {
            Node n = new Node(item);

            if (_head == null)
            {
                _head = _tail = n;
            }
            else
            {
                n.Next = _head;
                _head.Prev = n;
                _head = n;
            }

            _count++;
        }

        public void AddBack(T item)
        {
            Node n = new Node(item);

            if (_tail == null)
            {
                _head = _tail = n;
            }
            else
            {
                n.Prev = _tail;
                _tail.Next = n;
                _tail = n;
            }

            _count++;
        }

        public T RemoveFront()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");

            T val = _head.Value;
            _head = _head.Next;

            if (_head == null)
                _tail = null;
            else
                _head.Prev = null;

            _count--;
            return val;
        }

        public T RemoveBack()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");

            T val = _tail.Value;
            _tail = _tail.Prev;

            if (_tail == null)
                _head = null;
            else
                _tail.Next = null;

            _count--;
            return val;
        }

        public T PeekFront()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");
            return _head.Value;
        }

        public T PeekBack()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");
            return _tail.Value;
        }
    }
}
