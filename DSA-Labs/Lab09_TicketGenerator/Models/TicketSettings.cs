namespace Lab09_TicketGenerator.Models
{
    /// <summary>
    /// Модель настроек генерации билетов
    /// </summary>
    public class TicketSettings
    {
        /// <summary>
        /// Количество вопросов в одном билете
        /// </summary>
        public int QuestionsPerTicket { get; set; }

        /// <summary>
        /// Максимальная суммарная сложность билета
        /// </summary>
        public int MaxTotalDifficulty { get; set; }

        /// <summary>
        /// Количество практических вопросов
        /// </summary>
        public int PracticeCount { get; set; }

        /// <summary>
        /// Количество теоретических вопросов
        /// </summary>
        public int LectureCount { get; set; }

        /// <summary>
        /// Количество блиц-вопросов
        /// </summary>
        public int BlitzCount { get; set; }

        /// <summary>
        /// Конструктор с настройками по умолчанию
        /// </summary>
        public TicketSettings()
        {
            QuestionsPerTicket = 4;
            MaxTotalDifficulty = 20;
            PracticeCount = 1;
            LectureCount = 2;
            BlitzCount = 1;
        }
    }
}