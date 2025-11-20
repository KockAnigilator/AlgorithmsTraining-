using System;
using Lab06_10_Collections.Stack.Interfaces;

namespace Lab06_10_Collections.Stack.Implementations
{
    /// <summary>
    /// Стек, реализованный на односвязном списке.
    /// Push/Pop работают с головой списка.
    /// </summary>
    public class LinkedStack<T> : IStack<T>
    {
        private Node _head;
        private int _count;

        /// <summary>
        /// Внутренний узел списка.
        /// </summary>
        private sealed class Node
        {
            public T Value;
            public Node Next;

            public Node(T value, Node next)
            {
                Value = value;
                Next = next;
            }
        }

        /// <summary>
        /// Количество элементов в стеке.
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// Добавляет элемент в голову списка.
        /// Сложность: O(1).
        /// </summary>
        public void Push(T item)
        {
            _head = new Node(item, _head);
            _count++;
        }

        /// <summary>
        /// Удаляет и возвращает элемент из головы списка.
        /// Сложность: O(1).
        /// </summary>
        public T Pop()
        {
            if (_head == null)
                throw new InvalidOperationException("Stack is empty.");

            T val = _head.Value;
            _head = _head.Next;
            _count--;
            return val;
        }

        /// <summary>
        /// Возвращает элемент из головы без удаления.
        /// Сложность: O(1).
        /// </summary>
        public T Peek()
        {
            if (_head == null)
                throw new InvalidOperationException("Stack is empty.");

            return _head.Value;
        }

        /// <summary>
        /// Проверяет, пуст ли стек.
        /// </summary>
        public bool IsEmpty()
        {
            return _head == null;
        }
    }
}
