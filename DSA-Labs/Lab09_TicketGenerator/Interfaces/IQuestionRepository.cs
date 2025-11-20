using System.Collections.Generic;
using Lab09_TicketGenerator.Models;

namespace Lab09_TicketGenerator.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работы с вопросами
    /// </summary>
    public interface IQuestionRepository
    {
        /// <summary>
        /// Добавляет новый вопрос в базу данных
        /// </summary>
        void AddQuestion(Question question);

        /// <summary>
        /// Обновляет существующий вопрос
        /// </summary>
        void UpdateQuestion(Question question);

        /// <summary>
        /// Удаляет вопрос по идентификатору
        /// </summary>
        void DeleteQuestion(int id);

        /// <summary>
        /// Получает все вопросы из базы данных
        /// </summary>
        List<Question> GetAllQuestions();

        /// <summary>
        /// Получает вопросы по типу
        /// </summary>
        List<Question> GetQuestionsByType(QuestionType type);

        /// <summary>
        /// Получает вопросы по теме
        /// </summary>
        List<Question> GetQuestionsByTopic(string topic);

        /// <summary>
        /// Получает только активные вопросы
        /// </summary>
        List<Question> GetActiveQuestions();

        /// <summary>
        /// Получает все уникальные темы
        /// </summary>
        List<string> GetAllTopics();

        /// <summary>
        /// Активирует/деактивирует вопрос
        /// </summary>
        void SetQuestionActive(int id, bool isActive);
    }
}