using System.Collections.Generic;

namespace Lab09_TicketGenerator.Models
{
    /// <summary>
    /// Модель экзаменационного билета
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Идентификатор билета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Список вопросов в билете
        /// </summary>
        public List<Question> Questions { get; set; }

        /// <summary>
        /// Общая сложность билета
        /// </summary>
        public int TotalDifficulty { get; set; }

        /// <summary>
        /// Название билета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Ticket()
        {
            Questions = new List<Question>();
        }
    }
}