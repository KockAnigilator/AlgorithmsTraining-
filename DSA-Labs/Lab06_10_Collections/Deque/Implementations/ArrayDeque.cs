using System;
using Lab06_10_Collections.Deque.Interfaces;

namespace Lab06_10_Collections.Deque.Implementations
{
    /// <summary>
    /// Реализация deque на базе кольцевого буфера.
    /// </summary>
    public class ArrayDeque<T> : IDeque<T>
    {
        private T[] _data;
        private int _head;
        private int _tail;
        private int _count;

        private const int DefaultCapacity = 4;

        public int Count { get { return _count; } }

        public ArrayDeque(int capacity = DefaultCapacity)
        {
            if (capacity <= 0) capacity = DefaultCapacity;

            _data = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        public bool IsEmpty() { return _count == 0; }

        public void AddFront(T item)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);

            _head = (_head - 1 + _data.Length) % _data.Length;
            _data[_head] = item;
            _count++;
        }

        public void AddBack(T item)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);

            _data[_tail] = item;
            _tail = (_tail + 1) % _data.Length;
            _count++;
        }

        public T RemoveFront()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");

            T val = _data[_head];
            _data[_head] = default(T);
            _head = (_head + 1) % _data.Length;
            _count--;

            return val;
        }

        public T RemoveBack()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");

            _tail = (_tail - 1 + _data.Length) % _data.Length;
            T val = _data[_tail];
            _data[_tail] = default(T);
            _count--;

            return val;
        }

        public T PeekFront()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");
            return _data[_head];
        }

        public T PeekBack()
        {
            if (_count == 0)
                throw new InvalidOperationException("Deque is empty.");

            int index = (_tail - 1 + _data.Length) % _data.Length;
            return _data[index];
        }

        private void Resize(int newSize)
        {
            T[] arr = new T[newSize];

            for (int i = 0; i < _count; i++)
                arr[i] = _data[(_head + i) % _data.Length];

            _data = arr;
            _head = 0;
            _tail = _count;
        }
    }
}
