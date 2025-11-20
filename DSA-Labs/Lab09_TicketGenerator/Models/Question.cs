namespace Lab09_TicketGenerator.Models
{
    /// <summary>
    /// Модель вопроса для экзаменационных билетов
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Уникальный идентификатор вопроса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип вопроса (Практика, Лекция, Блиц)
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// Тема вопроса
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Сложность вопроса (от 1 до 10)
        /// </summary>
        public int Difficulty { get; set; }

        /// <summary>
        /// Флаг активности вопроса
        /// </summary>
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Типы вопросов
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// Практический вопрос
        /// </summary>
        Practice,

        /// <summary>
        /// Теоретический вопрос
        /// </summary>
        Lecture,

        /// <summary>
        /// Блиц-вопрос
        /// </summary>
        Blitz
    }
}