using System;
using Lab06_10_Collections.List.Interfaces;

namespace Lab06_10_Collections.List.Implementations
{
    /// <summary>
    /// Двусвязный список с эффективными операциями на концах.
    /// </summary>
    public class DoublyLinkedList<T> : ILinkedList<T>
    {
        private Node _head;
        private Node _tail;
        private int _count;

        private sealed class Node
        {
            public T Value;
            public Node Prev;
            public Node Next;
            public Node(T v) { Value = v; Prev = Next = null; }
        }

        public int Count { get { return _count; } }

        public bool IsEmpty() { return _head == null; }

        public void AddFirst(T value)
        {
            var n = new Node(value);
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

        public void AddLast(T value)
        {
            var n = new Node(value);
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

        public T RemoveFirst()
        {
            if (_head == null) throw new InvalidOperationException("List is empty.");
            T v = _head.Value;
            _head = _head.Next;
            if (_head != null) _head.Prev = null;
            else _tail = null;
            _count--;
            return v;
        }

        public T RemoveLast()
        {
            if (_tail == null) throw new InvalidOperationException("List is empty.");
            T v = _tail.Value;
            _tail = _tail.Prev;
            if (_tail != null) _tail.Next = null;
            else _head = null;
            _count--;
            return v;
        }

        public bool Remove(T value)
        {
            Node cur = _head;
            while (cur != null)
            {
                if (Equals(cur.Value, value))
                {
                    if (cur.Prev != null) cur.Prev.Next = cur.Next;
                    else _head = cur.Next;

                    if (cur.Next != null) cur.Next.Prev = cur.Prev;
                    else _tail = cur.Prev;

                    _count--;
                    return true;
                }
                cur = cur.Next;
            }
            return false;
        }

        public bool Contains(T value)
        {
            Node cur = _head;
            while (cur != null)
            {
                if (Equals(cur.Value, value)) return true;
                cur = cur.Next;
            }
            return false;
        }
    }
}
