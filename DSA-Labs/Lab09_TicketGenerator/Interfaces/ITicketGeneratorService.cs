using System.Collections.Generic;
using Lab09_TicketGenerator.Models;

namespace Lab09_TicketGenerator.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для генерации билетов
    /// </summary>
    public interface ITicketGeneratorService
    {
        /// <summary>
        /// Генерирует билеты по указанному режиму
        /// </summary>
        List<Ticket> GenerateTickets(int count, string examMode);

        /// <summary>
        /// Генерирует билеты в стандартном режиме
        /// </summary>
        List<Ticket> GenerateTicketsStandard(int count);

        /// <summary>
        /// Генерирует билеты в сбалансированном режиме
        /// </summary>
        List<Ticket> GenerateTicketsBalanced(int count);

        /// <summary>
        /// Генерирует билеты в смешанном режиме
        /// </summary>
        List<Ticket> GenerateTicketsMixed(int count);

        /// <summary>
        /// Проверяет возможность генерации билетов с текущими настройками
        /// </summary>
        bool CanGenerateTickets();
    }
}