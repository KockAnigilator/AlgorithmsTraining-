using System;

namespace Lab06_10_Collections.Stack.Interfaces
{
    /// <summary>
    /// Интерфейс абстрактного стека.
    /// Определяет минимальный набор операций LIFO-структуры.
    /// </summary>
    /// <typeparam name="T">Тип элементов, хранимых в стеке.</typeparam>
    public interface IStack<T>
    {
        /// <summary>
        /// Добавляет элемент на вершину стека.
        /// Теоретическая сложность: O(1).
        /// </summary>
        void Push(T item);

        /// <summary>
        /// Удаляет и возвращает элемент с вершины стека.
        /// Бросает InvalidOperationException, если стек пуст.
        /// Теоретическая сложность: O(1).
        /// </summary>
        T Pop();

        /// <summary>
        /// Возвращает элемент с вершины без удаления.
        /// Бросает InvalidOperationException, если стек пуст.
        /// Теоретическая сложность: O(1).
        /// </summary>
        T Peek();

        /// <summary>
        /// Возвращает true, если стек не содержит элементов.
        /// </summary>
        bool IsEmpty();

        /// <summary>
        /// Текущее количество элементов в стеке.
        /// </summary>
        int Count { get; }
    }
}
