using System;
using System.Collections.Generic;
using System.Linq;
using Lab09_TicketGenerator.Interfaces;
using Lab09_TicketGenerator.Models;

namespace Lab09_TicketGenerator.Services
{
    /// <summary>
    /// Сервис для генерации экзаменационных билетов
    /// </summary>
    public class TicketGeneratorService : ITicketGeneratorService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ITicketSettingsRepository _settingsRepository;
        private readonly Random _random;

        /// <summary>
        /// Конструктор сервиса генерации билетов
        /// </summary>
        public TicketGeneratorService(IQuestionRepository questionRepository, ITicketSettingsRepository settingsRepository)
        {
            _questionRepository = questionRepository;
            _settingsRepository = settingsRepository;
            _random = new Random();
        }

        /// <summary>
        /// Генерирует билеты по указанному режиму
        /// </summary>
        public List<Ticket> GenerateTickets(int count, string examMode)
        {
            if (!CanGenerateTickets())
            {
                throw new InvalidOperationException("Недостаточно вопросов для генерации билетов. Проверьте настройки и активные вопросы.");
            }

            if (examMode == "balanced")
            {
                return GenerateTicketsBalanced(count);
            }
            else if (examMode == "mixed")
            {
                return GenerateTicketsMixed(count);
            }
            else
            {
                return GenerateTicketsStandard(count);
            }
        }

        /// <summary>
        /// Генерирует билеты в стандартном режиме
        /// </summary>
        public List<Ticket> GenerateTicketsStandard(int count)
        {
            var settings = _settingsRepository.GetSettings();
            var activeQuestions = _questionRepository.GetActiveQuestions();
            var tickets = new List<Ticket>();

            for (int i = 0; i < count; i++)
            {
                var ticket = GenerateSingleTicket(settings, activeQuestions, i + 1);
                if (ticket != null)
                {
                    tickets.Add(ticket);
                }
            }

            return tickets;
        }

        /// <summary>
        /// Генерирует билеты в сбалансированном режиме (равные темы)
        /// </summary>
        public List<Ticket> GenerateTicketsBalanced(int count)
        {
            var settings = _settingsRepository.GetSettings();
            var activeQuestions = _questionRepository.GetActiveQuestions();
            var tickets = new List<Ticket>();
            var topics = _questionRepository.GetAllTopics();

            if (topics.Count == 0)
            {
                throw new InvalidOperationException("Нет доступных тем для генерации билетов.");
            }

            for (int i = 0; i < count; i++)
            {
                var ticket = GenerateBalancedTicket(settings, activeQuestions, topics, i + 1);
                if (ticket != null)
                {
                    tickets.Add(ticket);
                }
            }

            return tickets;
        }

        /// <summary>
        /// Генерирует билеты в смешанном режиме (больше практики)
        /// </summary>
        public List<Ticket> GenerateTicketsMixed(int count)
        {
            var settings = _settingsRepository.GetSettings();
            // В смешанном режиме увеличиваем количество практических вопросов
            settings.PracticeCount = 2;
            settings.LectureCount = 1;
            settings.BlitzCount = 1;
            settings.QuestionsPerTicket = 4;

            var activeQuestions = _questionRepository.GetActiveQuestions();
            var tickets = new List<Ticket>();

            for (int i = 0; i < count; i++)
            {
                var ticket = GenerateSingleTicket(settings, activeQuestions, i + 1);
                if (ticket != null)
                {
                    tickets.Add(ticket);
                }
            }

            return tickets;
        }

        /// <summary>
        /// Проверяет возможность генерации билетов с текущими настройками
        /// </summary>
        public bool CanGenerateTickets()
        {
            var settings = _settingsRepository.GetSettings();
            var activeQuestions = _questionRepository.GetActiveQuestions();

            // Проверяем, достаточно ли вопросов каждого типа
            var practiceQuestions = activeQuestions.Count(q => q.Type == QuestionType.Practice);
            var lectureQuestions = activeQuestions.Count(q => q.Type == QuestionType.Lecture);
            var blitzQuestions = activeQuestions.Count(q => q.Type == QuestionType.Blitz);

            return practiceQuestions >= settings.PracticeCount &&
                   lectureQuestions >= settings.LectureCount &&
                   blitzQuestions >= settings.BlitzCount;
        }

        /// <summary>
        /// Генерирует один билет в стандартном режиме
        /// </summary>
        private Ticket GenerateSingleTicket(TicketSettings settings, List<Question> allQuestions, int ticketNumber)
        {
            var ticket = new Ticket { Name = $"Билет {ticketNumber}" };
            var usedQuestions = new HashSet<int>();
            int totalDifficulty = 0;

            // Добавляем вопросы по типам согласно настройкам
            totalDifficulty = AddQuestionsByType(ticket, usedQuestions, totalDifficulty,
                allQuestions, QuestionType.Practice, settings.PracticeCount, settings.MaxTotalDifficulty);

            totalDifficulty = AddQuestionsByType(ticket, usedQuestions, totalDifficulty,
                allQuestions, QuestionType.Lecture, settings.LectureCount, settings.MaxTotalDifficulty);

            totalDifficulty = AddQuestionsByType(ticket, usedQuestions, totalDifficulty,
                allQuestions, QuestionType.Blitz, settings.BlitzCount, settings.MaxTotalDifficulty);

            ticket.TotalDifficulty = totalDifficulty;

            // Проверяем, что билет содержит нужное количество вопросов
            if (ticket.Questions.Count < settings.QuestionsPerTicket)
            {
                return null; // Не удалось сгенерировать полный билет
            }

            return ticket;
        }

        /// <summary>
        /// Генерирует один билет в сбалансированном режиме
        /// </summary>
        private Ticket GenerateBalancedTicket(TicketSettings settings, List<Question> allQuestions, List<string> topics, int ticketNumber)
        {
            var ticket = new Ticket { Name = $"Билет {ticketNumber}" };
            var usedQuestions = new HashSet<int>();
            int totalDifficulty = 0;

            // Перемешиваем темы для равномерного распределения
            var shuffledTopics = topics.OrderBy(x => _random.Next()).ToList();

            // Распределяем вопросы по темам
            foreach (var topic in shuffledTopics.Take(settings.QuestionsPerTicket))
            {
                var availableQuestions = allQuestions
                    .Where(q => q.Topic == topic && !usedQuestions.Contains(q.Id))
                    .OrderBy(q => q.Difficulty)
                    .ToList();

                if (availableQuestions.Any())
                {
                    var question = availableQuestions.First();
                    if (totalDifficulty + question.Difficulty <= settings.MaxTotalDifficulty)
                    {
                        ticket.Questions.Add(question);
                        usedQuestions.Add(question.Id);
                        totalDifficulty += question.Difficulty;
                    }
                }
            }

            ticket.TotalDifficulty = totalDifficulty;

            if (ticket.Questions.Count < settings.QuestionsPerTicket)
            {
                return null; // Не удалось сгенерировать полный билет
            }

            return ticket;
        }

        /// <summary>
        /// Добавляет вопросы определенного типа в билет
        /// </summary>
        private int AddQuestionsByType(Ticket ticket, HashSet<int> usedQuestions, int totalDifficulty,
            List<Question> allQuestions, QuestionType type, int count, int maxDifficulty)
        {
            int currentDifficulty = totalDifficulty;

            for (int i = 0; i < count; i++)
            {
                var available = allQuestions
                    .Where(q => q.Type == type && !usedQuestions.Contains(q.Id)
                        && currentDifficulty + q.Difficulty <= maxDifficulty)
                    .OrderBy(q => q.Difficulty)
                    .ToList();

                if (available.Any())
                {
                    var question = available.First();
                    ticket.Questions.Add(question);
                    usedQuestions.Add(question.Id);
                    currentDifficulty += question.Difficulty;
                }
            }

            return currentDifficulty;
        }

        /// <summary>
        /// Получает статистику по доступным вопросам
        /// </summary>
        public Dictionary<string, int> GetQuestionsStatistics()
        {
            var activeQuestions = _questionRepository.GetActiveQuestions();

            return new Dictionary<string, int>
            {
                ["Practice"] = activeQuestions.Count(q => q.Type == QuestionType.Practice),
                ["Lecture"] = activeQuestions.Count(q => q.Type == QuestionType.Lecture),
                ["Blitz"] = activeQuestions.Count(q => q.Type == QuestionType.Blitz),
                ["Total"] = activeQuestions.Count,
                ["Topics"] = _questionRepository.GetAllTopics().Count
            };
        }
    }
}