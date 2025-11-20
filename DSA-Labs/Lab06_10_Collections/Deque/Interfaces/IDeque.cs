using System;

namespace Lab06_10_Collections.Deque.Interfaces
{
    /// <summary>
    /// Интерфейс двусторонней очереди (deque).
    /// Поддерживает вставку и удаление с обоих концов.
    /// </summary>
    public interface IDeque<T>
    {
        /// <summary>Добавляет элемент в начало.</summary>
        void AddFront(T item);

        /// <summary>Добавляет элемент в конец.</summary>
        void AddBack(T item);

        /// <summary>Удаляет и возвращает элемент из начала.</summary>
        T RemoveFront();

        /// <summary>Удаляет и возвращает элемент из конца.</summary>
        T RemoveBack();

        /// <summary>Возвращает элемент из начала без удаления.</summary>
        T PeekFront();

        /// <summary>Возвращает элемент из конца без удаления.</summary>
        T PeekBack();

        /// <summary>Проверяет, пуст ли deque.</summary>
        bool IsEmpty();

        /// <summary>Количество элементов.</summary>
        int Count { get; }
    }
}
