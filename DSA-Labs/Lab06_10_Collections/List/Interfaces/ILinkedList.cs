using System;

namespace Lab06_10_Collections.List.Interfaces
{
    /// <summary>
    /// Интерфейс одно/двусвязного списка (минимальный набор операций).
    /// </summary>
    /// <typeparam name="T">Тип элементов списка.</typeparam>
    public interface ILinkedList<T>
    {
        /// <summary>Добавляет элемент в начало списка. O(1).</summary>
        void AddFirst(T value);

        /// <summary>Добавляет элемент в конец списка. O(1).</summary>
        void AddLast(T value);

        /// <summary>Удаляет и возвращает первый элемент. Бросает, если пуст. O(1).</summary>
        T RemoveFirst();

        /// <summary>Удаляет и возвращает последний элемент. Бросает, если пуст. O(1) для двусвязного списка, O(n) для однонаправленного.</summary>
        T RemoveLast();

        /// <summary>Удаляет первое вхождение value. Возвращает true, если удалено. O(n).</summary>
        bool Remove(T value);

        /// <summary>Проверяет наличие элемента. O(n).</summary>
        bool Contains(T value);

        /// <summary>Пуст список? O(1).</summary>
        bool IsEmpty();

        /// <summary>Количество элементов в списке. O(1).</summary>
        int Count { get; }
    }
}
