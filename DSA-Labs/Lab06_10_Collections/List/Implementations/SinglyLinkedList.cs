using System;
using Lab06_10_Collections.List.Interfaces;

namespace Lab06_10_Collections.List.Implementations
{
    /// <summary>
    /// Простая реализация односвязного списка.
    /// Поддерживает AddFirst, AddLast, RemoveFirst, RemoveLast (O(n)), Remove, Contains.
    /// </summary>
    public class SinglyLinkedList<T> : ILinkedList<T>
    {
        private Node _head;
        private Node _tail;
        private int _count;

        private sealed class Node
        {
            public T Value;
            public Node Next;
            public Node(T v) { Value = v; Next = null; }
        }

        public int Count { get { return _count; } }

        public bool IsEmpty() { return _head == null; }

        public void AddFirst(T value)
        {
            var n = new Node(value);
            n.Next = _head;
            _head = n;
            if (_tail == null) _tail = n;
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
            if (_head == null) _tail = null;
            _count--;
            return v;
        }

        public T RemoveLast()
        {
            if (_head == null) throw new InvalidOperationException("List is empty.");

            // Если только один элемент
            if (_head == _tail)
            {
                T v = _head.Value;
                _head = _tail = null;
                _count = 0;
                return v;
            }

            // Идём до предпоследнего
            Node cur = _head;
            while (cur.Next != _tail) cur = cur.Next;

            T val = _tail.Value;
            cur.Next = null;
            _tail = cur;
            _count--;
            return val;
        }

        public bool Remove(T value)
        {
            Node prev = null;
            Node cur = _head;
            while (cur != null)
            {
                if (Equals(cur.Value, value))
                {
                    if (prev == null)
                    {
                        // удаление головы
                        _head = cur.Next;
                        if (_head == null) _tail = null;
                    }
                    else
                    {
                        prev.Next = cur.Next;
                        if (cur == _tail) _tail = prev;
                    }
                    _count--;
                    return true;
                }
                prev = cur;
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