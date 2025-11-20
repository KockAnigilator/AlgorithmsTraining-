using System;
using Lab06_10_Collections.Stack.Interfaces;

namespace Lab06_10_Collections.Stack.Implementations
{
    /// <summary>
    /// Реализация стека на основе динамического массива.
    /// Push выполняется в конец массива, Resize удваивает размер.
    /// </summary>
    /// <typeparam name="T">Тип элементов.</typeparam>
    public class ArrayStack<T> : IStack<T>
    {
        private T[] _data;
        private int _count;

        private const int DefaultCapacity = 4;

        /// <summary>
        /// Текущее количество элементов.
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// Создаёт стек с начальной вместимостью.
        /// </summary>
        public ArrayStack(int capacity = DefaultCapacity)
        {
            if (capacity <= 0) capacity = DefaultCapacity;
            _data = new T[capacity];
            _count = 0;
        }

        /// <summary>
        /// Добавляет элемент на вершину.
        /// Амортизированная сложность: O(1).
        /// </summary>
        public void Push(T item)
        {
            if (_count == _data.Length)
                Resize(_data.Length * 2);

            _data[_count++] = item;
        }

        /// <summary>
        /// Возвращает и удаляет верхний элемент.
        /// Сложность: O(1).
        /// </summary>
        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty.");

            _count--;
            T item = _data[_count];
            _data[_count] = default(T); // очистка для GC
            return item;
        }

        /// <summary>
        /// Возвращает верхний элемент без удаления.
        /// Сложность: O(1).
        /// </summary>
        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty.");

            return _data[_count - 1];
        }

        /// <summary>
        /// Проверяет, пуст ли стек.
        /// </summary>
        public bool IsEmpty()
        {
            return _count == 0;
        }

        /// <summary>
        /// Увеличивает размер массива.
        /// </summary>
        private void Resize(int newSize)
        {
            T[] arr = new T[newSize];
            Array.Copy(_data, arr, _count);
            _data = arr;
        }
    }
}
