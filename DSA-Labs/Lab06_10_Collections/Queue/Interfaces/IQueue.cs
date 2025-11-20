using System;

namespace Lab06_10_Collections.Queue.Interfaces
{
    /// <summary>
    /// Интерфейс очереди (FIFO — First In First Out).
    /// </summary>
    /// <typeparam name="T">Тип хранимых элементов.</typeparam>
    public interface IQueue<T>
    {
        /// <summary>
        /// Добавляет элемент в конец очереди.
        /// Теоретическая сложность: O(1) амортизированно.
        /// </summary>
        void Enqueue(T item);

        /// <summary>
        /// Удаляет и возвращает элемент из начала очереди.
        /// Бросает InvalidOperationException, если очередь пуста.
        /// Теоретическая сложность: O(1).
        /// </summary>
        T Dequeue();

        /// <summary>
        /// Возвращает элемент из начала очереди без удаления.
        /// </summary>
        T Peek();

        /// <summary>
        /// Проверяет, пуста ли очередь.
        /// </summary>
        bool IsEmpty();

        /// <summary>
        /// Количество элементов в очереди.
        /// </summary>
        int Count { get; }
    }
}
