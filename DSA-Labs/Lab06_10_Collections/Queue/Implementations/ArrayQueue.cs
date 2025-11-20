using System;
using Lab06_10_Collections.Queue.Interfaces;

namespace Lab06_10_Collections.Queue.Implementations
{
    /// <summary>
    /// Очередь на основе кольцевого буфера.
    /// Вставка в tail, удаление из head.
    /// </summary>
    public class ArrayQueue<T> : IQueue<T>
    {
        private T[] _data;
        private int _head;
        private int _tail;
        private int _count;

        private const int DefaultCapacity = 4;

        public int Count { get { return _count; } }

        public ArrayQueue(int capacity = DefaultCapacity)
        {
            if (capacity <= 0) capacity = DefaultCapacity;
            _data = new T[capacity];
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        public void Enqueue(T item)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);

            _data[_tail] = item;
            _tail = (_tail + 1) % _data.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");

            T val = _data[_head];
            _data[_head] = default(T);
            _head = (_head + 1) % _data.Length;
            _count--;
            return val;
        }

        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Queue is empty.");

            return _data[_head];
        }

        public bool IsEmpty()
        {
            return _count == 0;
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
